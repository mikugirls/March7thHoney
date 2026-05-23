using March7thHoney.Data;
using March7thHoney.Database;
using March7thHoney.Database.Quests;
using March7thHoney.Database.TrainCakeCatch;
using March7thHoney.Database.Player;
using March7thHoney.Enums.Quest;
using March7thHoney.GameServer.Game.Player;
using March7thHoney.GameServer.Server;
using March7thHoney.GameServer.Server.Packet;
using March7thHoney.Kcp;
using March7thHoney.GameServer.Server.Packet.Send.PlayerSync;
using March7thHoney.Proto;
using March7thHoney.Util;
using System.Collections.Concurrent;

namespace March7thHoney.GameServer.Game.TrainCakeCatch;

public class TrainCakeCatchManager(PlayerInstance player) : BasePlayerManager(player)
{
    private sealed class SocialPlayReturnScene
    {
        public required int PlaneId { get; init; }
        public required int FloorId { get; init; }
        public required int EntryId { get; init; }
        public required Position Pos { get; init; }
        public required Position Rot { get; init; }
    }

    private sealed class SocialPlayRoom
    {
        public required uint OwnerUid { get; init; }
        public HashSet<int> Members { get; } = [];
        public Dictionary<int, MACJBIAFNOI> Motions { get; } = [];
        public Dictionary<int, List<MACJBIAFNOI>> MotionHistory { get; } = [];
        public List<EHKEJEPNGMB> ArrivalHistory { get; } = [];
        public SocialPlaySceneContext? SceneContext { get; set; }
    }

    public sealed class SocialPlaySceneContext
    {
        public required int PlaneId { get; init; }
        public required int FloorId { get; init; }
        public required int EntryId { get; init; }
        public required Position Pos { get; init; }
        public required Position Rot { get; init; }
    }

    private static readonly object RoomLock = new();
    private static readonly ConcurrentDictionary<uint, SocialPlayRoom> Rooms = new();
    private const int JoinedRoomGameplayType = 550;
    private const int OwnRoomGameplayType = 4650;
    private const int MaxBufferedMotionsPerPlayer = 20;

    public TrainCakeCatchData Data { get; } =
        DatabaseHelper.Instance!.GetInstanceOrCreateNew<TrainCakeCatchData>(player.Uid);
    public uint? CurrentRoomOwnerUid { get; private set; }
    public uint? PendingRoomOwnerUid { get; private set; }
    private SocialPlayReturnScene? ReturnScene { get; set; }

    private static int GetConst(string name, int fallback)
    {
        return GameData.CakeCatchConstValueData.TryGetValue(name, out var entry)
               && entry.Value.IntValue is { } v
            ? v
            : fallback;
    }

    private static long WeekEndUnixSec()
    {
        var now = DateTimeOffset.Now;
        var daysUntilSunday = ((int)DayOfWeek.Sunday - (int)now.DayOfWeek + 7) % 7;
        var weekEnd = now.Date.AddDays(daysUntilSunday).AddDays(1).AddSeconds(-1);
        return new DateTimeOffset(weekEnd, now.Offset).ToUnixTimeSeconds();
    }

    private static long NextDayUnixSec()
    {
        var now = DateTimeOffset.Now;
        var nextDay = now.Date.AddDays(1);
        return new DateTimeOffset(nextDay, now.Offset).ToUnixTimeSeconds();
    }

    public void EnsureDefaults()
    {
        EnsureDefaults(Data, Player.Uid);
    }

    private void EnsureDefaults(TrainCakeCatchData target, int targetUid)
    {
        var changed = false;
        var nowUnixSec = Extensions.GetUnixSec();

        if (target.RefreshTime == 0)
        {
            target.RefreshTime = WeekEndUnixSec();
            changed = true;
        }

        if (target.DailyRefreshTime == 0)
        {
            target.DailyRefreshTime = NextDayUnixSec();
            changed = true;
        }

        if (target.DailyRefreshTime <= nowUnixSec)
        {
            target.DailyRefreshTime = NextDayUnixSec();
            target.AvailableSearchCount = 1;
            changed = true;
        }

        if (target.SearchCreatureIds.SequenceEqual([1, 2, 3, 4, 5])
            && target.CollectedCreatures.Count == 0
            && target.AvailableSearchCount == 0)
        {
            target.SearchCreatureIds.Clear();
            changed = true;
        }

        if (target.AvailableSearchCount == 0 && target.SearchCreatureIds.Count == 0)
        {
            target.AvailableSearchCount = 1;
            changed = true;
        }

        changed |= SyncCollectedCreatures(target);

        if (changed)
            DatabaseHelper.ToSaveUidList.Add(targetUid);
    }

    public TrainCakeCatchGetDataScRsp ToGetDataProto()
    {
        return ToGetDataProto(CurrentRoomOwnerUid);
    }

    public TrainCakeCatchGetDataScRsp ToGetDataProto(uint? roomOwnerUid)
    {
        var view = ResolveRoomView(roomOwnerUid);
        EnsureDefaults(view.Data, (int)view.OwnerUid);
        EnsureDefaults();
        MKHIPFHEKCK socialPlayRoomData;

        lock (RoomLock)
        {
            socialPlayRoomData = Rooms.TryGetValue(view.OwnerUid, out var room)
                ? BuildSocialPlayRoomDataLocked(room, view.OwnerUid, (uint)Player.Uid)
                : BuildSocialPlayRoomData(view.OwnerUid, (uint)Player.Uid);
        }

        var rsp = new TrainCakeCatchGetDataScRsp
        {
            DHLGCEGONIB = view.Data.RefreshTime,
            PEOECDDANNP = BuildDiyProto(view.Data),
            FMECAHALPKG = socialPlayRoomData,
            PEPMFGLDGOH = new FAOPPAFAJLO(),
            HJOFGOINOAK = new LLKEMMMEPOB
            {
                EHLIFHPILPG = (uint)Math.Max(0, Data.AvailableSearchCount),
                FIHMHJHGHOG = Data.DailyRefreshTime
            }
        };

        rsp.PerformanceIdList.AddRange(GetUnlockedPerformanceIds(Data).Select(x => (uint)x));

        rsp.AFEJACMLOLH.AddRange(view.Data.CatTreeSlots.Select(x => new NPLEDHFMFDM
        {
            HCJPDNDOHAM = (uint)x.CreatureId,
            Slot = (uint)x.Slot
        }));

        foreach (var entry in GetCollectedCreatures(Data))
        {
            rsp.BLEDIOOJPNL.Add(new PKILNDNCAMI
            {
                HCJPDNDOHAM = (uint)entry.CreatureId,
                Count = (uint)entry.Count
            });
        }

        foreach (var creatureId in Data.SearchCreatureIds)
            rsp.KALIBGCODNF.Add(new LGGABHPGPDD { HCJPDNDOHAM = (uint)creatureId });

        return rsp;
    }

    public uint GetGameplayType(uint roomOwnerUid, uint roomPlayerUid)
    {
        return (uint)(roomOwnerUid == roomPlayerUid
            ? OwnRoomGameplayType
            : JoinedRoomGameplayType);
    }

    public long GetRoomRefreshTime(uint? roomOwnerUid = null)
    {
        var view = ResolveRoomView(roomOwnerUid);
        EnsureDefaults(view.Data, (int)view.OwnerUid);
        return view.Data.RefreshTime;
    }

    public async ValueTask<MKHIPFHEKCK> EnterSocialPlayRoomAsync(uint roomOwnerUid)
    {
        var motion = CreateMotion(Player.Data.Pos, Player.Data.Rot, true);
        List<int> existingMembers;
        List<int> previousRoomMembers = [];
        uint? previousRoomOwnerUid = null;
        MKHIPFHEKCK snapshot;
        lock (RoomLock)
        {
            if (CurrentRoomOwnerUid == roomOwnerUid
                && Rooms.TryGetValue(roomOwnerUid, out var currentRoom)
                && currentRoom.Members.Contains(Player.Uid))
            {
                RecordMotionLocked(currentRoom, Player.Uid, motion);
                PendingRoomOwnerUid = null;
                return BuildSocialPlayRoomDataLocked(currentRoom, roomOwnerUid, (uint)Player.Uid);
            }

            if (CurrentRoomOwnerUid is { } previousRoom)
            {
                previousRoomOwnerUid = previousRoom;
                if (Rooms.TryGetValue(previousRoom, out var previousRoomData))
                    previousRoomMembers = previousRoomData.Members.Where(uid => uid != Player.Uid).ToList();

                LeaveSocialPlayRoomInternal(previousRoom, Player.Uid);
            }
            else
            {
                BookmarkReturnScene();
            }

            var room = Rooms.GetOrAdd(roomOwnerUid, ownerUid => new SocialPlayRoom { OwnerUid = ownerUid });
            existingMembers = room.Members.Where(uid => uid != Player.Uid).ToList();
            room.Members.Add(Player.Uid);
            RecordMotionLocked(room, Player.Uid, motion);
            if (roomOwnerUid != (uint)Player.Uid)
                AppendArrivalHistoryLocked(room, Player.Uid, Player.Data.Name ?? string.Empty);

            CurrentRoomOwnerUid = roomOwnerUid;
            PendingRoomOwnerUid = null;
            snapshot = BuildSocialPlayRoomDataLocked(room, roomOwnerUid, (uint)Player.Uid);
        }

        if (previousRoomOwnerUid.HasValue)
            await NotifyRoomLeaveAsync(previousRoomOwnerUid.Value, previousRoomMembers, roomOwnerUid);

        await NotifyRoomJoinAsync(roomOwnerUid, existingMembers);
        return snapshot;
    }

    public void PrepareSocialPlayRoom(uint roomOwnerUid)
    {
        lock (RoomLock)
        {
            if (CurrentRoomOwnerUid == null)
                BookmarkReturnScene();

            PendingRoomOwnerUid = roomOwnerUid == 0 ? (uint)Player.Uid : roomOwnerUid;
        }
    }

    public void LeaveSocialPlayRoom()
    {
        List<int> remainingMembers = [];
        uint? leavingRoomOwnerUid = null;
        lock (RoomLock)
        {
            if (CurrentRoomOwnerUid is not { } roomOwnerUid)
                return;

            PendingRoomOwnerUid = null;
            leavingRoomOwnerUid = roomOwnerUid;
            if (Rooms.TryGetValue(roomOwnerUid, out var room))
                remainingMembers = room.Members.Where(uid => uid != Player.Uid).ToList();

            LeaveSocialPlayRoomInternal(roomOwnerUid, Player.Uid);
            CurrentRoomOwnerUid = null;
        }

        if (leavingRoomOwnerUid.HasValue)
            _ = NotifyRoomLeaveAsync(leavingRoomOwnerUid.Value, remainingMembers);
    }

    public async ValueTask<MKHIPFHEKCK> AttachToSocialPlayRoomAsync(uint roomOwnerUid)
    {
        var motion = CreateMotion(Player.Data.Pos, Player.Data.Rot, true);
        List<int> existingMembers = [];
        List<int> previousRoomMembers = [];
        uint? previousRoomOwnerUid = null;
        MKHIPFHEKCK snapshot;
        var shouldNotifyJoin = false;

        lock (RoomLock)
        {
            if (CurrentRoomOwnerUid == roomOwnerUid)
            {
                var currentRoom = Rooms.GetOrAdd(roomOwnerUid, ownerUid => new SocialPlayRoom { OwnerUid = ownerUid });
                existingMembers = currentRoom.Members.Where(uid => uid != Player.Uid).ToList();
                var wasMember = currentRoom.Members.Contains(Player.Uid);
                currentRoom.Members.Add(Player.Uid);
                RecordMotionLocked(currentRoom, Player.Uid, motion);
                PendingRoomOwnerUid = null;
                snapshot = BuildSocialPlayRoomDataLocked(currentRoom, roomOwnerUid, (uint)Player.Uid);
                shouldNotifyJoin = !wasMember;
            }
            else
            {
                if (CurrentRoomOwnerUid == null)
                    BookmarkReturnScene();
                else
                {
                    previousRoomOwnerUid = CurrentRoomOwnerUid;
                    if (previousRoomOwnerUid.HasValue
                        && Rooms.TryGetValue(previousRoomOwnerUid.Value, out var previousRoom))
                    {
                        previousRoomMembers = previousRoom.Members.Where(uid => uid != Player.Uid).ToList();
                        LeaveSocialPlayRoomInternal(previousRoomOwnerUid.Value, Player.Uid);
                    }
                }

                var room = Rooms.GetOrAdd(roomOwnerUid, ownerUid => new SocialPlayRoom { OwnerUid = ownerUid });
                existingMembers = room.Members.Where(uid => uid != Player.Uid).ToList();
                room.Members.Add(Player.Uid);
                RecordMotionLocked(room, Player.Uid, motion);
                CurrentRoomOwnerUid = roomOwnerUid;
                PendingRoomOwnerUid = null;
                snapshot = BuildSocialPlayRoomDataLocked(room, roomOwnerUid, (uint)Player.Uid);
                shouldNotifyJoin = true;
            }
        }

        if (previousRoomOwnerUid.HasValue)
            await NotifyRoomLeaveAsync(previousRoomOwnerUid.Value, previousRoomMembers, roomOwnerUid);

        if (shouldNotifyJoin)
            await NotifyRoomJoinAsync(roomOwnerUid, existingMembers);

        return snapshot;
    }

    public uint? GetSceneTeleportRoomOwnerUid()
    {
        lock (RoomLock)
        {
            return PendingRoomOwnerUid ?? CurrentRoomOwnerUid;
        }
    }

    public async ValueTask<bool> ReturnToBookedSceneAsync(bool sendPacket)
    {
        PendingRoomOwnerUid = null;
        LeaveSocialPlayRoom();

        if (ReturnScene == null)
            return false;

        var returnScene = ReturnScene;
        ReturnScene = null;
        await Player.LoadScene(returnScene.PlaneId, returnScene.FloorId, returnScene.EntryId,
            returnScene.Pos, returnScene.Rot, sendPacket);
        return true;
    }

    public SocialPlaySceneContext? GetCurrentRoomSceneContext()
    {
        if (CurrentRoomOwnerUid is not { } roomOwnerUid)
            return null;

        return GetRoomSceneContext(roomOwnerUid);
    }

    public SocialPlaySceneContext? GetRoomSceneContext(uint roomOwnerUid)
    {
        lock (RoomLock)
        {
            if (!Rooms.TryGetValue(roomOwnerUid, out var room))
                return null;

            return room.SceneContext == null
                ? null
                : new SocialPlaySceneContext
                {
                    PlaneId = room.SceneContext.PlaneId,
                    FloorId = room.SceneContext.FloorId,
                    EntryId = room.SceneContext.EntryId,
                    Pos = room.SceneContext.Pos,
                    Rot = room.SceneContext.Rot
                };
        }
    }

    public void UpdateCurrentRoomSceneContext()
    {
        if (CurrentRoomOwnerUid is not { } roomOwnerUid
            || Player.Data.Pos == null
            || Player.Data.Rot == null
            || Player.Data.PlaneId == 0
            || Player.Data.FloorId == 0
            || Player.Data.EntryId == 0)
            return;

        lock (RoomLock)
        {
            if (!Rooms.TryGetValue(roomOwnerUid, out var room))
                return;

            if (room.OwnerUid != (uint)Player.Uid && room.SceneContext != null)
                return;

            room.SceneContext = new SocialPlaySceneContext
            {
                PlaneId = Player.Data.PlaneId,
                FloorId = Player.Data.FloorId,
                EntryId = Player.Data.EntryId,
                Pos = Player.Data.Pos,
                Rot = Player.Data.Rot
            };
        }
    }

    public SocialPlayGameplayOperationScNotify? BuildArrivalHistoryNotify(uint roomOwnerUid)
    {
        lock (RoomLock)
        {
            if (!Rooms.TryGetValue(roomOwnerUid, out var room) || room.ArrivalHistory.Count == 0)
                return null;

            var popup = new HMDJNPFBOMM();
            popup.IMBCJKHMOAB.AddRange(room.ArrivalHistory.Select(entry => entry.Clone()));
            return new SocialPlayGameplayOperationScNotify
            {
                MFBDMLPDEOE = roomOwnerUid,
                NCFHGIBCEBG = roomOwnerUid,
                AFCCFJOEIOH = popup
            };
        }
    }

    public async ValueTask BroadcastPlayerStateAsync()
    {
        if (CurrentRoomOwnerUid is not { } roomOwnerUid)
            return;

        HashSet<int> recipientUids = [];
        int? implicitOwnerUid = null;
        MACJBIAFNOI? currentMotion = null;
        lock (RoomLock)
        {
            if (!Rooms.TryGetValue(roomOwnerUid, out var room))
                return;

            room.Motions.TryGetValue(Player.Uid, out currentMotion);
            foreach (var uid in room.Members.Where(uid => uid != Player.Uid))
                recipientUids.Add(uid);

            if (roomOwnerUid != (uint)Player.Uid && !room.Members.Contains((int)roomOwnerUid))
                implicitOwnerUid = (int)roomOwnerUid;
        }

        TryAddImplicitOwnerRecipient(recipientUids, implicitOwnerUid, roomOwnerUid, Player.Uid);

        if (recipientUids.Count == 0)
            return;

        var playerStateNotify = new SocialPlayGameplayOperationScNotify
        {
            MFBDMLPDEOE = roomOwnerUid,
            NCFHGIBCEBG = (uint)Player.Uid,
            PEFPFCINABN = BuildSocialPlayPlayer((uint)Player.Uid, false)
        };
        playerStateNotify.PEFPFCINABN.IACKOCPGIAK = BuildSimpleMotion(currentMotion, Player.Data.Pos, Player.Data.Rot);

        foreach (var uid in recipientUids)
        {
            var target = Listener.GetActiveConnection(uid)?.Player;
            if (target?.Connection?.IsOnline != true)
                continue;

            var packet = new BasePacket(CmdIds.SocialPlayGameplayOperationScNotify);
            packet.SetData(playerStateNotify.Clone());
            await target.SendPacket(packet);
        }
    }

    public MKHIPFHEKCK BuildSocialPlayRoomData(uint roomOwnerUid, uint roomPlayerUid)
    {
        var view = ResolveRoomView(roomOwnerUid);
        EnsureDefaults(view.Data, (int)view.OwnerUid);
        var gameplayType = GetGameplayType(roomOwnerUid, roomPlayerUid);
        var snapshot = new MKHIPFHEKCK();
        snapshot.MFBDMLPDEOE = roomOwnerUid;
        snapshot.DFJBNHPAICA = new GFOIOIACDCH
        {
            DMGCIDGKPFF = new MCPPIEJEBEF
            {
                LEKGBMCOJDB = new NKLAFFLJCHG()
            },
            PAIBKOMPFOE = gameplayType,
            KPDHCPCCPNA = new GCOIFAHPGDF
            {
                DHLGCEGONIB = view.Data.RefreshTime,
                GJGAGFEJABC = (uint)Math.Max(1, view.Data.DiyTheme),
                PEOECDDANNP = BuildDiyProto(view.Data)
            }
        };

        if (gameplayType == OwnRoomGameplayType)
        {
            snapshot.DFJBNHPAICA.KPDHCPCCPNA.FIHMHJHGHOG = view.Data.DailyRefreshTime;
            snapshot.DFJBNHPAICA.KPDHCPCCPNA.GIIIBCKEODP = (uint)Math.Max(1, view.Data.CatTreeSlots.Count + 1);
        }

        snapshot.DFJBNHPAICA.KPDHCPCCPNA.AFEJACMLOLH.Add(view.Data.CatTreeSlots.Select(x => new NPLEDHFMFDM
        {
            HCJPDNDOHAM = (uint)x.CreatureId,
            Slot = (uint)x.Slot
        }));
        snapshot.PlayerInfo.Clear();

        if (roomPlayerUid == roomOwnerUid)
        {
            snapshot.PlayerInfo.Add(BuildPlaceholderSocialPlayPlayer(roomPlayerUid, true));
        }
        else
        {
            snapshot.PlayerInfo.Add(BuildSocialPlayPlayer(roomPlayerUid, true));
            snapshot.PlayerInfo.Add(BuildPlaceholderSocialPlayPlayer(roomOwnerUid, false));
        }

        return snapshot;
    }

    public async ValueTask HandleRoomMovement(MKLFLKDHLIC req)
    {
        if (CurrentRoomOwnerUid is not { } roomOwnerUid || req.IACKOCPGIAK == null)
            return;

        List<int> otherMembers;
        int? implicitOwnerUid = null;
        MACJBIAFNOI motion;

        lock (RoomLock)
        {
            if (!Rooms.TryGetValue(roomOwnerUid, out var room))
                return;

            motion = req.IACKOCPGIAK.Clone();
            RecordMotionLocked(room, Player.Uid, motion);
            otherMembers = room.Members.Where(uid => uid != Player.Uid).ToList();

            if (roomOwnerUid != (uint)Player.Uid && !room.Members.Contains((int)roomOwnerUid))
                implicitOwnerUid = (int)roomOwnerUid;
        }

        var ackPacket = new BasePacket(CmdIds.IPCKGLOHJLH);
        ackPacket.SetData(new IPCKGLOHJLH());
        await Player.SendPacket(ackPacket);

        var notify = new SocialPlayRoomPlayerMoveScNotify
        {
            MFBDMLPDEOE = roomOwnerUid,
            NCFHGIBCEBG = (uint)Player.Uid,
            FIPIDGKPFHA = new CCKCCKHEFFB
            {
                NCFHGIBCEBG = (uint)Player.Uid,
                FIPIDGKPFHA = { motion }
            }
        };

        var recipientUids = new HashSet<int>(otherMembers);
        if (roomOwnerUid != (uint)Player.Uid)
            TryAddImplicitOwnerRecipient(recipientUids, implicitOwnerUid, roomOwnerUid, Player.Uid);

        foreach (var memberUid in recipientUids)
        {
            var target = Listener.GetActiveConnection(memberUid)?.Player;
            if (target?.Connection?.IsOnline != true)
                continue;

            var packet = new BasePacket(CmdIds.SocialPlayRoomPlayerMoveScNotify);
            packet.SetData(notify.Clone());
            await target.SendPacket(packet);
        }
    }

    public IReadOnlyList<SocialPlayRoomPlayerMoveScNotify> BuildInitialRoomMoveNotifies(uint roomOwnerUid)
    {
        var notifies = new List<SocialPlayRoomPlayerMoveScNotify>();

        lock (RoomLock)
        {
            if (Rooms.TryGetValue(roomOwnerUid, out var room))
            {
                foreach (var entry in room.Motions.Where(x => x.Key != Player.Uid).OrderBy(x => x.Key))
                {
                    var notify = new SocialPlayRoomPlayerMoveScNotify
                    {
                        MFBDMLPDEOE = roomOwnerUid,
                        NCFHGIBCEBG = (uint)entry.Key,
                        FIPIDGKPFHA = new CCKCCKHEFFB
                        {
                            NCFHGIBCEBG = (uint)entry.Key
                        }
                    };
                    notify.FIPIDGKPFHA.FIPIDGKPFHA.Add(entry.Value.Clone());
                    notifies.Add(notify);
                }
            }
        }

        return notifies;
    }

    public MKHIPFHEKCK BuildRoomSnapshotForPlayer(uint roomOwnerUid, uint roomPlayerUid)
    {
        lock (RoomLock)
        {
            return Rooms.TryGetValue(roomOwnerUid, out var room)
                ? BuildSocialPlayRoomDataLocked(room, roomOwnerUid, roomPlayerUid)
                : BuildSocialPlayRoomData(roomOwnerUid, roomPlayerUid);
        }
    }

    public async ValueTask BroadcastRoomSnapshotAsync()
    {
        if (CurrentRoomOwnerUid is not { } roomOwnerUid)
            return;

        List<int> recipients;
        lock (RoomLock)
        {
            if (!Rooms.TryGetValue(roomOwnerUid, out var room))
                return;

            recipients = room.Members.Where(uid => uid != Player.Uid).ToList();
        }

        foreach (var uid in recipients)
        {
            var target = Listener.GetActiveConnection(uid)?.Player;
            if (target?.Connection?.IsOnline != true)
                continue;

            var packet = new BasePacket(CmdIds.SocialPlayGameplayOperationScNotify);
            packet.SetData(new SocialPlayGameplayOperationScNotify
            {
                MFBDMLPDEOE = roomOwnerUid,
                NCFHGIBCEBG = (uint)uid,
                INPEGNFEPAP = BuildRoomSnapshotForPlayer(roomOwnerUid, (uint)uid)
            });
            await target.SendPacket(packet);
        }
    }

    public FOFNOBHLKGO BuildDiyProto()
    {
        return BuildDiyProto(Data);
    }

    public FOFNOBHLKGO BuildDiyProto(TrainCakeCatchData source)
    {
        var diy = new FOFNOBHLKGO
        {
            MIPPCFJPJDD = (HEAJBHNMJGO)source.DiyTheme
        };

        diy.OJNJEDBBAJJ.AddRange(source.DiceSlots.Select(x =>
        {
            var msg = new BIAIKHBFALH
            {
                DiceSlotId = (uint)x.DiceSlotId,
                HGJHIJOGGIN = (uint)x.Index
            };
            msg.GBGDLNNOBID.AddRange(x.Values.Select(v => (uint)v));
            return msg;
        }));

        diy.LKPGMBDBJFA.AddRange(source.StagePlacements.Select(x => new GFAHEGCPIEB
        {
            HCJPDNDOHAM = (uint)x.CreatureId,
            Slot = (uint)x.Slot
        }));

        return diy;
    }

    public FOFNOBHLKGO ApplyDiy(FOFNOBHLKGO req)
    {
        EnsureDefaults();
        Data.DiyTheme = (int)req.MIPPCFJPJDD;

        Data.DiceSlots = req.OJNJEDBBAJJ.Select(x => new DiyDiceSlotInfo
        {
            DiceSlotId = (int)x.DiceSlotId,
            Index = (int)x.HGJHIJOGGIN,
            Values = x.GBGDLNNOBID.Select(v => (int)v).ToList()
        }).ToList();

        Data.StagePlacements = req.LKPGMBDBJFA.Select(x => new DiyStagePlacement
        {
            CreatureId = (int)x.HCJPDNDOHAM,
            Slot = (int)x.Slot
        }).ToList();

        SyncCollectedCreatures(Data);
        DatabaseHelper.ToSaveUidList.Add(Player.Uid);
        return BuildDiyProto();
    }

    public TrainCakeCatchSearchScRsp Search(uint creatureId)
    {
        EnsureDefaults();
        var creatureIdInt = (int)creatureId;
        var unlockedBeforeSearch = GetUnlockedPerformanceIds(Data);

        AddCollectedCreature(Data, creatureIdInt);
        if (!Data.SearchCreatureIds.Contains(creatureIdInt))
            Data.SearchCreatureIds.Add(creatureIdInt);

        if (Data.AvailableSearchCount > 0)
            Data.AvailableSearchCount--;

        var unlockedAfterSearch = GetUnlockedPerformanceIds(Data);
        Data.PerformanceIds = unlockedAfterSearch.ToList();

        DatabaseHelper.ToSaveUidList.Add(Player.Uid);
        var rsp = new TrainCakeCatchSearchScRsp
        {
            HCJPDNDOHAM = creatureId,
            BLEDIOOJPNL = new PKILNDNCAMI
            {
                HCJPDNDOHAM = creatureId,
                Count = (uint)GetCollectedCreatureCount(Data, creatureIdInt)
            }
        };

        rsp.OMFCGLJDFPD.AddRange(unlockedAfterSearch
            .Except(unlockedBeforeSearch)
            .Select(x => (uint)x));
        rsp.KALIBGCODNF.AddRange(Data.SearchCreatureIds.Select(x => new LGGABHPGPDD
        {
            HCJPDNDOHAM = (uint)x
        }));
        return rsp;
    }

    public TrainCakeCatchOpenBoxScRsp OpenBox()
    {
        EnsureDefaults();

        if (Data.CatTreeSlots.Count == 0)
        {
            Data.CatTreeSlots =
            [
                new CakeTreeSlotInfo { CreatureId = 10, Slot = 4 },
                new CakeTreeSlotInfo { CreatureId = 21, Slot = 6 },
                new CakeTreeSlotInfo { CreatureId = 26, Slot = 5 }
            ];
        }

        DatabaseHelper.ToSaveUidList.Add(Player.Uid);

        var rsp = new TrainCakeCatchOpenBoxScRsp
        {
            DHLGCEGONIB = Data.RefreshTime
        };

        rsp.AFEJACMLOLH.AddRange(Data.CatTreeSlots.Select(x => new NPLEDHFMFDM
        {
            HCJPDNDOHAM = (uint)x.CreatureId,
            Slot = (uint)x.Slot
        }));

        return rsp;
    }

    public TrainCakeCatchDiyLikeScRsp LikeDiy()
    {
        EnsureDefaults();

        if (!Data.DiyLikeIds.Contains(Player.Uid))
            Data.DiyLikeIds.Add(Player.Uid);

        DatabaseHelper.ToSaveUidList.Add(Player.Uid);

        var rsp = new TrainCakeCatchDiyLikeScRsp();
        rsp.JNNKJAEMOBN.AddRange(Data.DiyLikeIds.Select(x => (uint)x));
        return rsp;
    }

    public async ValueTask UnlockAllPerformanceQuests()
    {
        var qm = Player.QuestManager;
        if (qm == null) return;

        var changed = new List<QuestInfo>();
        foreach (var perf in GameData.CakePerformanceConfigData.Values)
        {
            if (perf.QuestID == 0) continue;
            if (!qm.QuestData.Quests.TryGetValue(perf.QuestID, out var info))
            {
                info = new QuestInfo { QuestId = perf.QuestID };
                qm.QuestData.Quests[perf.QuestID] = info;
            }

            if (info.QuestStatus is QuestStatus.QuestFinish or QuestStatus.QuestClose) continue;

            info.QuestStatus = QuestStatus.QuestFinish;
            info.FinishTime = DateTime.Now.ToUnixSec();
            info.Progress = 1;
            changed.Add(info);
        }

        if (changed.Count == 0) return;

        DatabaseHelper.ToSaveUidList.Add(Player.Uid);
        await Player.SendPacket(new PacketPlayerSyncScNotify(changed));
    }

    public void SetCatTreeSlots(IEnumerable<(int CreatureId, int Slot)> placements)
    {
        Data.CatTreeSlots = placements.Select(p => new CakeTreeSlotInfo
        {
            CreatureId = p.CreatureId,
            Slot = p.Slot
        }).ToList();
        DatabaseHelper.ToSaveUidList.Add(Player.Uid);
    }

    private OEBJBPNNOKO BuildSocialPlayPlayer(uint uid, bool isSelf)
    {
        var data = ResolveSocialPlayPlayer(uid);
        return new OEBJBPNNOKO
        {
            Uid = (uint)data.Uid,
            SummonedPetId = data.Pet > 0 ? (uint)data.Pet : 0,
            CGLMHJFCFPH = data.ToSimpleProto(FriendOnlineStatus.Online),
            EKODIFJOBFB = Math.Max(0, data.LastActiveTime),
            HCJMLMIGACG = true,
            IACKOCPGIAK = CreateMotion(data.Pos, data.Rot, isSelf)
        };
    }

    private OEBJBPNNOKO BuildPlaceholderSocialPlayPlayer(uint uid, bool isSelf)
    {
        var data = ResolveSocialPlayPlayer(uid);
        var proto = new OEBJBPNNOKO
        {
            Uid = (uint)data.Uid,
            CGLMHJFCFPH = data.ToSimpleProto(FriendOnlineStatus.Online),
            IACKOCPGIAK = CreatePlaceholderMotion()
        };

        if (isSelf && data.Pet > 0)
            proto.SummonedPetId = (uint)data.Pet;

        return proto;
    }

    private void AppendFullSnapshotPlayer(MKHIPFHEKCK snapshot, SocialPlayRoom room, HashSet<int> addedUids, int memberUid,
        bool isSelf)
    {
        if (!addedUids.Add(memberUid))
            return;

        var proto = BuildSocialPlayPlayer((uint)memberUid, isSelf);
        if (room.Motions.TryGetValue(memberUid, out var motion))
            proto.IACKOCPGIAK = BuildSnapshotMotion(motion, proto.IACKOCPGIAK, isSelf);
        snapshot.PlayerInfo.Add(proto);
    }

    private void AppendPlaceholderSnapshotPlayer(MKHIPFHEKCK snapshot, HashSet<int> addedUids, int memberUid,
        bool isSelf)
    {
        if (!addedUids.Add(memberUid))
            return;

        snapshot.PlayerInfo.Add(BuildPlaceholderSocialPlayPlayer((uint)memberUid, isSelf));
    }

    private static void TryAddImplicitOwnerRecipient(HashSet<int> recipientUids, int? implicitOwnerUid,
        uint roomOwnerUid, int senderUid)
    {
        if (!implicitOwnerUid.HasValue || implicitOwnerUid.Value == senderUid)
            return;

        var ownerPlayer = Listener.GetActiveConnection(implicitOwnerUid.Value)?.Player;
        if (ownerPlayer?.Connection?.IsOnline == true
            && ownerPlayer.TrainCakeCatchManager?.CurrentRoomOwnerUid == roomOwnerUid)
        {
            recipientUids.Add(implicitOwnerUid.Value);
        }
    }

    private static EHKEJEPNGMB CreateArrivalEntry(uint uid, string name, long? time = null)
    {
        return new EHKEJEPNGMB
        {
            NINNEKFGNLI = 1,
            NCFHGIBCEBG = uid,
            GNCOPJNBCKI = time ?? Extensions.GetUnixSec(),
            OFOMBFJEDKC = name
        };
    }

    private static void AppendArrivalHistoryLocked(SocialPlayRoom room, int uid, string name)
    {
        room.ArrivalHistory.Add(CreateArrivalEntry((uint)uid, name));
        if (room.ArrivalHistory.Count > 20)
            room.ArrivalHistory.RemoveRange(0, room.ArrivalHistory.Count - 20);
    }

    private static void RecordMotionLocked(SocialPlayRoom room, int uid, MACJBIAFNOI motion)
    {
        room.Motions[uid] = motion.Clone();

        if (!room.MotionHistory.TryGetValue(uid, out var history))
        {
            history = [];
            room.MotionHistory[uid] = history;
        }

        history.Add(motion.Clone());
        if (history.Count > MaxBufferedMotionsPerPlayer)
            history.RemoveRange(0, history.Count - MaxBufferedMotionsPerPlayer);
    }

    private static MACJBIAFNOI? GetCurrentMotion(uint roomOwnerUid, int uid)
    {
        lock (RoomLock)
        {
            if (!Rooms.TryGetValue(roomOwnerUid, out var room))
                return null;

            return room.Motions.TryGetValue(uid, out var motion)
                ? motion.Clone()
                : null;
        }
    }

    private MKHIPFHEKCK BuildSocialPlayRoomDataLocked(SocialPlayRoom room, uint roomOwnerUid, uint roomPlayerUid)
    {
        var snapshot = BuildSocialPlayRoomData(roomOwnerUid, roomPlayerUid);
        snapshot.PlayerInfo.Clear();
        var addedUids = new HashSet<int>();

        if (roomPlayerUid == roomOwnerUid)
        {
            foreach (var memberUid in room.Members
                         .Where(uid => uid != roomOwnerUid)
                         .OrderBy(uid => uid))
                AppendFullSnapshotPlayer(snapshot, room, addedUids, memberUid, false);

            if (room.Members.Contains((int)roomOwnerUid))
                AppendFullSnapshotPlayer(snapshot, room, addedUids, (int)roomOwnerUid, true);
            else
                AppendPlaceholderSnapshotPlayer(snapshot, addedUids, (int)roomOwnerUid, true);
        }
        else
        {
            if (room.Members.Contains((int)roomPlayerUid))
                AppendFullSnapshotPlayer(snapshot, room, addedUids, (int)roomPlayerUid, true);
            else
                AppendPlaceholderSnapshotPlayer(snapshot, addedUids, (int)roomPlayerUid, true);

            if (room.Members.Contains((int)roomOwnerUid))
                AppendFullSnapshotPlayer(snapshot, room, addedUids, (int)roomOwnerUid, false);
            else
                AppendPlaceholderSnapshotPlayer(snapshot, addedUids, (int)roomOwnerUid, false);

            foreach (var memberUid in room.Members
                         .Where(uid => uid != roomPlayerUid && uid != roomOwnerUid)
                         .OrderBy(uid => uid))
                AppendFullSnapshotPlayer(snapshot, room, addedUids, memberUid, false);
        }

        return snapshot;
    }

    private static IEnumerable<CakeCreatureInventoryInfo> GetCollectedCreatures(TrainCakeCatchData source)
    {
        return source.CollectedCreatures.Where(x => x.CreatureId > 0 && x.Count > 0);
    }

    private static int GetCollectedCreatureCount(TrainCakeCatchData source, int creatureId)
    {
        return source.CollectedCreatures.FirstOrDefault(x => x.CreatureId == creatureId)?.Count ?? 0;
    }

    private static void AddCollectedCreature(TrainCakeCatchData source, int creatureId)
    {
        if (creatureId <= 0)
            return;

        var existing = source.CollectedCreatures.FirstOrDefault(x => x.CreatureId == creatureId);
        if (existing != null)
        {
            existing.Count = Math.Max(1, existing.Count + 1);
            return;
        }

        source.CollectedCreatures.Add(new CakeCreatureInventoryInfo
        {
            CreatureId = creatureId,
            Count = 1
        });
    }

    private static bool SyncCollectedCreatures(TrainCakeCatchData source)
    {
        var changed = false;

        for (var i = source.CollectedCreatures.Count - 1; i >= 0; i--)
        {
            if (source.CollectedCreatures[i].CreatureId <= 0)
            {
                source.CollectedCreatures.RemoveAt(i);
                changed = true;
                continue;
            }

            if (source.CollectedCreatures[i].Count <= 0)
            {
                source.CollectedCreatures[i].Count = 1;
                changed = true;
            }
        }

        foreach (var placement in source.StagePlacements.Where(x => x.CreatureId > 0))
        {
            if (source.CollectedCreatures.Any(x => x.CreatureId == placement.CreatureId))
                continue;

            source.CollectedCreatures.Add(new CakeCreatureInventoryInfo
            {
                CreatureId = placement.CreatureId,
                Count = 1
            });
            changed = true;
        }

        return changed;
    }

    private static List<int> GetUnlockedPerformanceIds(TrainCakeCatchData source)
    {
        var ownedCreatureIds = GetCollectedCreatures(source)
            .Select(x => x.CreatureId)
            .ToHashSet();
        var unlockedPerformanceIds = new HashSet<int>(source.PerformanceIds);

        foreach (var entry in GameData.CakePerformanceConfigData.Values.OrderBy(x => x.ID))
        {
            if (entry.ActorsList.Count == 0)
                continue;

            if (entry.ActorsList.All(ownedCreatureIds.Contains))
                unlockedPerformanceIds.Add(entry.ID);
        }

        return unlockedPerformanceIds.OrderBy(x => x).ToList();
    }

    private (uint OwnerUid, TrainCakeCatchData Data) ResolveRoomView(uint? requestedOwnerUid)
    {
        var ownerUid = requestedOwnerUid.GetValueOrDefault((uint)Player.Uid);
        if (ownerUid == 0)
            ownerUid = (uint)Player.Uid;

        if (ownerUid == (uint)Player.Uid)
            return (ownerUid, Data);

        var activeOwner = Listener.GetActiveConnection((int)ownerUid)?.Player?.TrainCakeCatchManager;
        if (activeOwner != null)
            return (ownerUid, activeOwner.Data);

        var data = DatabaseHelper.Instance!.GetInstanceOrCreateNew<TrainCakeCatchData>((int)ownerUid);
        return (ownerUid, data);
    }

    private async System.Threading.Tasks.Task NotifyRoomJoinAsync(uint roomOwnerUid, List<int> existingMembers)
    {
        var recipientUids = new HashSet<int>(existingMembers.Where(uid => uid != Player.Uid));
        if (roomOwnerUid != (uint)Player.Uid)
            recipientUids.Add((int)roomOwnerUid);

        if (recipientUids.Count == 0)
            return;

        var joinedPlayer = BuildSocialPlayPlayer((uint)Player.Uid, false);
        joinedPlayer.IACKOCPGIAK = BuildSimpleMotion(GetCurrentMotion(roomOwnerUid, Player.Uid), Player.Data.Pos, Player.Data.Rot);
        var notify = new SocialPlayGameplayOperationScNotify
        {
            MFBDMLPDEOE = roomOwnerUid,
            NCFHGIBCEBG = (uint)Player.Uid,
            PEFPFCINABN = joinedPlayer
        };

        foreach (var uid in recipientUids)
        {
            var target = Listener.GetActiveConnection(uid)?.Player;
            if (target?.Connection?.IsOnline != true)
                continue;

            if ((uint)uid == roomOwnerUid && roomOwnerUid != (uint)Player.Uid)
            {
                var arrivalHistoryNotify = BuildArrivalHistoryNotify(roomOwnerUid);
                if (arrivalHistoryNotify != null)
                {
                    var arrivalPacket = new BasePacket(CmdIds.SocialPlayGameplayOperationScNotify);
                    arrivalPacket.SetData(arrivalHistoryNotify);
                    await target.SendPacket(arrivalPacket);
                }
            }

            var packet = new BasePacket(CmdIds.SocialPlayGameplayOperationScNotify);
            packet.SetData(notify.Clone());
            await target.SendPacket(packet);
        }
    }

    private async System.Threading.Tasks.Task NotifyRoomLeaveAsync(uint roomOwnerUid, List<int> remainingMembers,
        uint? nextRoomOwnerUid = null)
    {
        if (remainingMembers.Count == 0)
            return;

        var notify = new SocialPlayGameplayOperationScNotify
        {
            MFBDMLPDEOE = roomOwnerUid,
            NCFHGIBCEBG = (uint)Player.Uid,
            NEAIFLAADFE = (uint)Player.Uid
        };

        foreach (var uid in remainingMembers)
        {
            var target = Listener.GetActiveConnection(uid)?.Player;
            if (target?.Connection?.IsOnline != true)
                continue;

            var packet = new BasePacket(CmdIds.SocialPlayGameplayOperationScNotify);
            packet.SetData(notify.Clone());
            await target.SendPacket(packet);

            
            
            var snapshotPacket = new BasePacket(CmdIds.SocialPlayGameplayOperationScNotify);
            snapshotPacket.SetData(new SocialPlayGameplayOperationScNotify
            {
                MFBDMLPDEOE = roomOwnerUid,
                NCFHGIBCEBG = (uint)uid,
                INPEGNFEPAP = BuildRoomSnapshotForPlayer(roomOwnerUid, (uint)uid)
            });
            await target.SendPacket(snapshotPacket);
        }
    }

    private PlayerData ResolveSocialPlayPlayer(uint uid)
    {
        if (uid == Player.Uid)
            return Player.Data;

        var playerData = PlayerData.GetPlayerByUid(uid);
        if (playerData != null)
            return playerData;

        var serverProfile = ConfigManager.Config.ServerOption.ServerProfile;
        return new PlayerData
        {
            Uid = uid > 0 ? (int)uid : serverProfile.Uid,
            Name = serverProfile.Name,
            Signature = serverProfile.Signature,
            Level = serverProfile.Level,
            HeadIcon = serverProfile.HeadIcon,
            ChatBubble = serverProfile.ChatBubbleId,
            PersonalCard = serverProfile.PersonalCardId,
            LastActiveTime = Extensions.GetUnixSec()
        };
    }

    private static MACJBIAFNOI CreateMotion(Position? pos, Position? rot, bool isSelf)
    {
        var motion = new MACJBIAFNOI
        {
            Pos = (pos ?? new Position(3278, -14999, 22389)).ToProto(),
            Rot = (rot ?? new Position(0, 18426, 0)).ToProto()
        };

        if (!isSelf)
        {
            motion.GNGKNDCKKKC = 1;
            motion.JCBGHAODNDD = 2;
            motion.KHAJDKDHPGD = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            motion.NALBEFMOKIB = new Position(328, 0, -944).ToProto();
        }

        return motion;
    }

    private static MACJBIAFNOI CreatePlaceholderMotion()
    {
        return new MACJBIAFNOI
        {
            Pos = new Vector
            {
                Y = -15000,
                Z = 25000
            },
            Rot = new Vector
            {
                X = -180000
            }
        };
    }

    private static MACJBIAFNOI BuildSimpleMotion(MACJBIAFNOI? source, Position? fallbackPos, Position? fallbackRot)
    {
        return new MACJBIAFNOI
        {
            Pos = source?.Pos?.Clone() ?? (fallbackPos ?? new Position(3278, -14999, 22389)).ToProto(),
            Rot = source?.Rot?.Clone() ?? (fallbackRot ?? new Position(0, 18426, 0)).ToProto()
        };
    }

    private static MACJBIAFNOI BuildSnapshotMotion(MACJBIAFNOI? source, MACJBIAFNOI fallbackMotion, bool isSelf)
    {
        var fallbackPos = fallbackMotion.Pos != null ? new Position(fallbackMotion.Pos) : null;
        var fallbackRot = fallbackMotion.Rot != null ? new Position(fallbackMotion.Rot) : null;
        var motion = BuildSimpleMotion(source, fallbackPos, fallbackRot);

        if (isSelf)
        {
            if (source?.NALBEFMOKIB != null)
                motion.NALBEFMOKIB = source.NALBEFMOKIB.Clone();

            if (source?.JCBGHAODNDD != null)
                motion.JCBGHAODNDD = source.JCBGHAODNDD;

            if (source?.DCBBKFFHHDL != null)
                motion.DCBBKFFHHDL = source.DCBBKFFHHDL;

            if (source?.KHAJDKDHPGD != null)
                motion.KHAJDKDHPGD = source.KHAJDKDHPGD;

            if (source?.GNGKNDCKKKC != null)
                motion.GNGKNDCKKKC = source.GNGKNDCKKKC;

            return motion;
        }

        if (source?.NALBEFMOKIB != null)
            motion.NALBEFMOKIB = source.NALBEFMOKIB.Clone();

        if (source?.JCBGHAODNDD != null)
            motion.JCBGHAODNDD = source.JCBGHAODNDD;

        if (source?.DCBBKFFHHDL != null)
            motion.DCBBKFFHHDL = source.DCBBKFFHHDL;

        if (source?.KHAJDKDHPGD != null)
            motion.KHAJDKDHPGD = source.KHAJDKDHPGD;

        if (source?.GNGKNDCKKKC != null)
            motion.GNGKNDCKKKC = source.GNGKNDCKKKC;

        return motion;
    }

    private void BookmarkReturnScene()
    {
        if (ReturnScene != null
            || Player.Data.Pos == null
            || Player.Data.Rot == null
            || Player.Data.PlaneId == 0
            || Player.Data.FloorId == 0
            || Player.Data.EntryId == 0)
            return;

        ReturnScene = new SocialPlayReturnScene
        {
            PlaneId = Player.Data.PlaneId,
            FloorId = Player.Data.FloorId,
            EntryId = Player.Data.EntryId,
            Pos = Player.Data.Pos,
            Rot = Player.Data.Rot
        };
    }

    private static void LeaveSocialPlayRoomInternal(uint roomOwnerUid, int uid)
    {
        if (!Rooms.TryGetValue(roomOwnerUid, out var room))
            return;

        room.Members.Remove(uid);
        room.Motions.Remove(uid);
        room.MotionHistory.Remove(uid);
        if (room.Members.Count == 0)
            Rooms.TryRemove(roomOwnerUid, out _);
    }
}
