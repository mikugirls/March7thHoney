# History Commits

Repository commit history with contributor, title, and commit description.

## 4.2

- Contributor: Mar7thLover (Cyrene)
- Commit: `1976cfd67b45e94b089b0fe56021d08db9299160`
- Description:
  - (No additional commit description)

## March7thHoney

- Contributor: Mar7thLover (Cyrene)
- Commit: `d0931c05f12c7d0dfe2be84d3bf31f3cdcb5ed7e`
- Description:
  - (No additional commit description)

## Fix

- Contributor: Mar7thLover (Cyrene)
- Commit: `d7e8a67d42b67c7b0c94bca58069827e8f077590`
- Description:
  - (No additional commit description)

## ReadMe

- Contributor: Mar7thLover (Cyrene)
- Commit: `d45138ab15222fe56ccdfec027c8eed8594d4011`
- Description:
  - (No additional commit description)

## ResCache

- Contributor: Mar7thLover (Cyrene)
- Commit: `32ff7d5bedc60734ab35bd73d78c35d77fe27114`
- Description:
  - (No additional commit description)

## optimization

- Contributor: Mar7thLover (Cyrene)
- Commit: `828e9cc6b23816f054d873991aec65196a11e64e`
- Description:
  - (No additional commit description)

## Little things

- Contributor: Mar7thLover (Cyrene)
- Commit: `1f5a71172a7171a1ace0caf3e8931ca8fa6cedef`
- Description:
  - (No additional commit description)

## LockEquipmentScRsp

- Contributor: Mar7thLover (Cyrene)
- Commit: `7d1cd7a08ca0b953e357e5600ded240232505c41`
- Description:
  - (No additional commit description)

## Real omg fix

- Contributor: Mar7thLover (Cyrene)
- Commit: `60780122ccddd0bf23a1a5332f4c1ebf17577ef1`
- Description:
  - (No additional commit description)

## freesr-support

- Contributor: Mar7thLover (Cyrene)
- Commit: `5ea3eac06b26aa069d59e241a92d19a3c3e6887b`
- Description:
  - (No additional commit description)

## failback

- Contributor: Mar7thLover (Cyrene)
- Commit: `5e1217e77d40b70f67fb8ffaedae24280e17b091`
- Description:
  - (No additional commit description)

## Add MonthCard for fun

- Contributor: Mar7thLover (Cyrene)
- Commit: `2b55faa740b027870189d321b8657d4e14bef5cf`
- Description:
  - (No additional commit description)

## MonthCard outdate time

- Contributor: Mar7thLover (Cyrene)
- Commit: `f9b3fc84f09312b231a8a45bb7cbec1f52ec38d5`
- Description:
  - (No additional commit description)

## DailyActive

- Contributor: Mar7thLover (Cyrene)
- Commit: `3e12c02e64e81b9cd883355ab22b205b4f486ef2`
- Description:
  - (No additional commit description)

## feat: configurable default nickname and dynamic bottom-left watermark

- Contributor: March7thHoney
- Commit: `3b8e6eb1bd58a054a9dae50d778c4573cb9d978f`
- Description:
  - - Add ServerOption.DefaultNickname; applied to new players in PlayerInstance ctor before save
  - - Cache client-reported version (CNPRODWIN4.2.0 etc.) from query_gateway in ClientVersionCache
  - - Replace hardcoded base64 watermark Lua with virtual SendWatermarkLuaAsync hook
  - - Override hook in GameServer Connection: dynamically build Lua showing "{nickname}｜March7thHoney\n{version}｜ {uid}" with proper Lua string escaping
  - - Resend watermark Lua immediately after SetNickname/SetPlayerInfo so nickname changes take effect mid-session

## feat: gradient bottom-left watermark and ignore local-only files

- Contributor: March7thHoney
- Commit: `9640a5501cd7e0aa3748cda0c8a7293a51d75bc6`
- Description:
  - - Apply per-line linear gradient (#54C3F7 → #C673AA) to watermark text via Unity rich-text <color> tags; enable vt.supportRichText
  - - Move Lua string escaping to wrap the final composed text instead of individual fields
  - - Untrack Config/Hotfix.json and ignore local-only paths plus macOS AppleDouble metadata

## Merge pull request #1 dynamic-watermark

- Contributor: Mar7thLover (Cyrene)
- Commit: `2e7e0955bb65bab80a5ed86aa18bea4356141a81`
- Description:
  - feat: configurable default nickname and dynamic gradient watermark

## Fix chat

- Contributor: Mar7thLover (Cyrene)
- Commit: `e84fd37f8d769b5b2905635eaad5029a91ba9363`
- Description:
  - (No additional commit description)

## Basic Implement GridFight

- Contributor: Mar7thLover (Cyrene)
- Commit: `70c57a7627d96f5cdba344a60230c4e50e8e805b`
- Description:
  - (No additional commit description)

## refactor(GridFight): rebuild as state machine matching official capture

- Contributor: March7thHoney
- Commit: `b67ab8654a57b97735e3b54af19be7e2b48ff834`
- Description:
  - Reverse-engineered the official server's GridFight protocol from a
  - 1479-line packet capture and rebuilt the implementation as a real state
  - machine instead of hardcoded placeholders.
  - GridFightInstance becomes the source of truth: gold / lineup HP / section /
  - queue position / pending action / shop refresh count / equipment draft /
  - active portal buffs / battles finished. Helpers TryBuyGoods, TryRecycleRole,
  - TryRefreshShop, TryEquipFromDraft, RotateShop, MaterializeInitialBenchTeam,
  - OnBattleResolved drive the transitions. ToProto now emits the full nine
  - RogueCurrentGameInfo sections sourced from this state.
  - GridFightManager.BuildSyncUpdateNotify is dispatched by a new GridFightSyncKind
  - enum (Bootstrap, PendingAdvance, SelectEquip, BuyGoods, RefreshShop,
  - RecycleRole, BuyExp, PostBattle, NoOp) so each handler emits the specific
  - delta the client expects rather than replaying the bootstrap blob.
  - HandlerGridFightSelectRecommendEquipCsReq is now an oneof dispatcher with the
  - queue rules from the capture: PortalBuffReroll keeps queuePos, PortalBuffAction
  - +1 and triggers the seven-section confirmation sync (addGameItemInfo,
  - addForgeInfo, portalServerDataUpdate, itemValue + addRoleInfo x4 team
  - materialization, finishPendingActionPos, levelSyncInfo with monster wave,
  - locked syncLockInfo + RoundBeginAction pending), RoundBegin jumps +8 to
  - ReturnPreparation, and ReturnPreparation +1 with no new pending.
  - HandlePendingActionScRsp now echoes req.QueuePosition (was sending the
  - advanced value, which broke client UI progression).
  - PacketGridFightEnterBattleStageScRsp no longer depends on BattleInstance
  - (BattleManager.StartGridFightBattle returned null because StageCandidates
  - held equipment IDs not stage IDs). It builds a SceneBattleInfo directly from
  - GridFightInstance with the AFCMOOFGBPK (OGEOMDJIAGI) sub-message containing
  - BBDOCJGAEEJ / LFKBMDHKPFI / OIHHKOJFHFG / PIDIGFGKAMK / OOPPKDAFFDG / etc.
  - Static rogueGetInfo aligned with the capture: divisionId 10706 -> 10939,
  - GridFightComboWinNum and Exp.BPJAFBFFPPH dropped (capture sends them empty).
  - PacketGridFightGetDataScRsp omits FightCurrentInfo at the menu (no instance)
  - to match the capture.
  - New handlers wired in: ResumeGamePlay (was the original blocker - client
  - sent 8752 with no handler), BackToPrepareReq, FinishTutorial,
  - DestroyWeeklyReward, GetArchive, TakeWeeklyReward, four Talent variants,
  - SummonProjection, UpdateEquipTrackPriority. Misc handlers Recycle and BuyExp
  - now drive instance state; the rest emit NoOp sync to stop spamming the
  - bootstrap blob.
  - PacketGridFightEndBattleStageNotify takes a GridFightInstance and reads its
  - post-battle state. PacketGridFightMiscScRsp gains thirteen new wrappers for
  - the previously missing obfuscated cmds (KMDHLENLIMF, EMIDPCIJAEN,
  - MNBAAGCGICN, DJCCBIOKNOD, GDMIIBNJJEJ, GetArchive/Talent ScRsps,
  - SyncKeepWinCntNotify, WeeklyExtraSeasonExpScRsp, SeasonHandBookNotify).
  - PacketGridFightSyncUpdateResultScNotify gets a (player, kind, extra) overload
  - and a pre-built-notify overload for handlers that need full control.
  - QuitSettle now emits WeeklyExtraSeasonExp + SyncKeepWinCnt before SettleNotify
  - to match the capture's settlement sequence.

## Merge pull request #2

- Contributor: Mar7thLover (Cyrene)
- Commit: `c78cc77a2d902198475b8fde6f4539f7c5508c26`
- Description:
  - feat(GridFight): 货币战争初步实现

## fix(skill): align AdventureTriggerAttack with official server

- Contributor: March7thHoney
- Commit: `837465b1058c09ebbe4552d432554231ffd44722`
- Description:
  - - Add EnterBattleSelectTargetType field to AdventureTriggerAttack config
  - - Fall back to request-reported hit targets when AttackTargetType is missing
  - - Trigger battle when TriggerBattle=true OR EnterBattleSelectTargetType is set,
  -   so attack-type maze skills (Evanescia) engage combat while debuff-type ones
  -   (DanHengPT) only apply buffs without forcing battle

## fix(inventory): forward level and suppress per-relic sync in giveall

- Contributor: March7thHoney
- Commit: `d8424d0ae118fba9fffa3ce445b5093b8f7c2dd8`
- Description:
  - - AddItem relic branch hardcoded level=0 when calling HandleRelic, dropping
  -   the level argument and giving every relic at +0 regardless of l<n> flag
  - - HandleRelic always sent a per-item PlayerSyncScNotify via the AddItem(ItemData)
  -   overload, flooding the KCP buffer and stalling /ga relic after ~80 items
  - Pass level through and add an optional sync flag on HandleRelic; when sync=false
  - fall back to PutItem so the giveall loop bulk-syncs once at the end.

## fix(skill): trigger battle on normal-attack hits regardless of ability flags

- Contributor: March7thHoney
- Commit: `bb9c30cd6d65e8097f9c1261ac3773d3e5d99b99`
- Description:
  - Castorice (and other avatars) ship NormalAtk01 ability JSON without TriggerBattle
  - or EnterBattleSelectTargetType, so the previous strict check skipped battle
  - engagement and the client black-screened after a normal attack on a monster.
  - Open-world combat entry is driven by the client reporting hits via
  - assistMonsterEntityIdList, not by ability config flags. Add a third trigger
  - rule: SkillIndex == 0 (normal attack) with at least one alive monster target
  - forces battle engagement. MazeSkill (SkillIndex >= 1) still respects the
  - ability flags to avoid regressing buff-only skills like DanHengPT's seal.

## feat(quest): auto-finish SpecificChallengeFloorGetStars to unlock ChallengePeak

- Contributor: March7thHoney
- Commit: `2a5b73af02aec8342ff82673317f797fcb4cb886`
- Description:
  - The 漫游签证 menu entry for 异相仲裁 (ChallengePeak) is gated by four
  - prerequisite quests on the client: 2200503/4/5 with FinishWay type
  - SpecificChallengeFloorGetStars (Memory floor 12 / Story stage 4 / Boss
  - difficulty 4, all 3-star) and 2200506 which depends on the prior three
  - via FinishQuest. Without a handler for SpecificChallengeFloorGetStars,
  - 2200503/4/5 stayed QUEST_DOING forever and the menu showed locked.
  - Add the missing handler. On every heartbeat HandleAllFinishType invokes
  - it, pushing the quest progress to excel.Progress. Since the private
  - server already grants every legacy challenge full stars, treating these
  - quests as satisfied unconditionally matches expected state.

## Merge pull request #3 fix/skill-relic-quest

- Contributor: Mar7thLover (Cyrene)
- Commit: `5e8344a17205bcdc1d033832f3c02e1b8d588a8b`
- Description:
  - 修复秘技/战斗/遗器命令异常 + 异相仲裁解锁

## JsonCommand i18n

- Contributor: Mar7thLover (Cyrene)
- Commit: `605febaf050d1242f3210a2854ffce7d1fb7968c`
- Description:
  - (No additional commit description)

## Chore:Fix bug in TakeChallengeReward

- Contributor: Mar7thLover (Cyrene)
- Commit: `149e38d263dd09855a87acbd0a6d7adbcd31323f`
- Description:
  - (No additional commit description)

## Load i18n first

- Contributor: Mar7thLover (Cyrene)
- Commit: `117423dec0f211c7f081c8601eccdaef6e270a97`
- Description:
  - (No additional commit description)

## Add License Manager

- Contributor: Mar7thLover (Cyrene)
- Commit: `07294fdf007fcc2847ca5cbf5d7aa9cf632cfff2`
- Description:
  - (No additional commit description)

## Add License Manager

- Contributor: Mar7thLover (Cyrene)
- Commit: `c28930d4a382378611a14d97e56164de0d30537e`
- Description:
  - (No additional commit description)

## fix(player): allow new account login with female default protagonist

- Contributor: March7thHoney
- Commit: `5b3bb6a7c504ab550d9b9c06cffde401105eab9c`
- Description:
  - After the default protagonist was switched to 8002 (female warrior), new
  - account creation crashed with retcode 1201_1 (Value cannot be null) inside
  - LineupInfo.ToProto. Three issues compounded:
  - - AvatarManager.AddAvatar(8002) takes the path-variant branch and only
  -   attaches a PathInfo to an already-existing 8001 hero. With no prior
  -   AddAvatar(8001) call, FormalAvatars ends up without any trailblazer.
  - - LineupManager.AddAvatar coerces every avatarId > 8000 down to 8001,
  -   silently overriding 8002 in the lineup so the BaseAvatarId no longer
  -   matches anything in FormalAvatars.
  - - The hero entry's AvatarId stayed 8001 even after the female path was
  -   added, so CurMultiPathAvatarType still rendered the male model.
  - Fix:
  - - PlayerInstance.ctor: AddAvatar(8001) before AddAvatar(8002) so the base
  -   hero exists for the path attach, then keep the lineup pointing at the
  -   base 8001 and flip hero.AvatarId to 8002 to display the female variant.
  - - LineupManager.AddAvatar: only fall back to 8001 when the requested
  -   avatarId is missing from AvatarConfigData; valid trailblazer variants
  -   (8002/8004/...) are no longer coerced.

## fix(command): make /giveall avatar trigger paths and survive per-avatar errors

- Contributor: March7thHoney
- Commit: `4e05905516c64056eeed9f28a7fd0ebc1438a2c9`
- Description:
  - - Run GiveAllPath at the start so multipath avatars (Trailblazer
  -   variants, Mar 7th Hunt, etc.) are unlocked even if the avatar loop
  -   errors out partway.
  - - Replace the four GetFormalAvatar(...)! calls per iteration with a
  -   single null-checked lookup; previously a null dereference in the
  -   middle of the loop silently aborted the rest of the loop, the final
  -   bulk PlayerSync, and the success message because CommandManager
  -   invokes async handlers via reflection without awaiting.
  - - Wrap each iteration in a try/catch so one bad entry no longer kills
  -   the whole command, and the final FormalAvatars sync is guaranteed.

## chore(db): remove obsolete March7thHoney.db

- Contributor: March7thHoney
- Commit: `5c923f5ecbe4e185e5f4c3de24bd4ec6d5532513`
- Description:
  - Runtime config now points DatabaseName at CastoriceHoney.db, so the
  - old SQLite file in Config/Database is no longer used.

## Merge pull request #4 from Mar7thLover/fix/female-protagonist-and-giveall

- Contributor: Mar7thLover (Cyrene)
- Commit: `a89f64b0d89424c1b5eea06ab8547cfaae1b11bc`
- Description:
  - fix: female default protagonist login and /giveall avatar

## fix(challenge): correct ChallengeStory lineup mapping and stage-2 transition

- Contributor: March7thHoney
- Commit: `c443bf602da7c5734f7bc285eb414b76c308c82b`
- Description:
  - - Swap ABNDFKFIKCI/BKNKLEOCJNO mapping in StartChallenge handler:
  -   field 13 (BKNKLEOCJNO) is the first half (LineupChallenge),
  -   field 2 (ABNDFKFIKCI) is the second half (LineupChallenge2).
  -   Previous reversed mapping caused upper/lower lineups to be swapped in game.
  - - Rework ChallengeStoryInstance.AdvanceStage to always re-enter the scene
  -   (ChallengeStory configs always have MapEntranceID == MapEntranceID2);
  -   switch lineup before EnterScene so the new SceneInstance loads the
  -   stage-2 avatars and the boss group via ChallengeEntityLoader.
  - - Removes the stale-entity issue that caused RET_SCENE_ENTITY_NOT_EXIST
  -   (2600) when attacking the second-half boss.
  - Known issue: ChallengeStory final score calculation is still incorrect.

## fix(message): stub-finish unseen NPC message groups when missions disabled

- Contributor: March7thHoney
- Commit: `f9a3e5b652099dd09def9fe39d0db2fb5ffe37a7`
- Description:
  - When ServerOption.EnableMission is false, the whole mission system is
  - short-circuited, so phone-message-driven gates (e.g. 忘却之庭 unlock via
  - Pam's chat tied to MainMission 4010105 / 4010134) never advance and the
  - client keeps showing "完成任务「流光忆彩」解锁忘却之庭".
  - GetMessageGroup now returns MESSAGE_GROUP_FINISH (with all sections
  - marked MESSAGE_SECTION_FINISH) for any group the player has no data for,
  - matching the "everything is done" stub semantics already used by the
  - mission status path. Player-tracked groups keep their real state.

## Merge pull request #5 from Mar7thLover/fix/pure-fiction-and-moc-menu

- Contributor: Mar7thLover (Cyrene)
- Commit: `45203a83fd1cc381d53202a331e065613b68d93b`
- Description:
  - 修复虚构叙事下半进战 + 忘却之庭菜单显示

## fix(challenge): rebuild scene on ChallengeMemory stage-2 transition

- Contributor: March7thHoney
- Commit: `2c1744169e75f9652e8333810122eac708014318`
- Description:
  - - Always EnterScene with MapEntranceID2 (fallback to MapEntranceID) when
  -   advancing to the second half. All MoC configs (5xxx) have
  -   MapEntranceID == MapEntranceID2, so the previous branch only called
  -   MoveTo and never rebuilt the scene, leaving the stage-1 boss room and
  -   entities in place for the second half.
  - - Switch to LineupChallenge2 before EnterScene so the rebuilt
  -   SceneInstance loads stage-2 avatars and the boss group via
  -   ChallengeEntityLoader (CurrentStage == 2 → ChallengeMonsters2).
  - - Drop the redundant manual UnloadGroup/LoadGroup and SyncLineup calls;
  -   EnterScene already handles them.
  - Mirrors the ChallengeStory fix in c443bf60 for the same root cause.

## fix(skill): backfill character maze buffs after technique cast

- Contributor: March7thHoney
- Commit: `cd1316fbe10a245f4bf2b41b1e65930d3654dbc4`
- Description:
  - Some characters' technique ability JSON spawns a summon unit instead of
  - calling AddMazeBuff (e.g. Dahlia 1321 -> 132101, also affects 1305/1309/
  - 1312/1410/1415/8007). Without scene-side buff, BattleInstance can't pick
  - it up, so the technique buff is missing from battleInfo.buffList and the
  - in-battle effect never fires.
  - After the technique ability runs, scan AvatarMazeBuff entries belonging
  - to the caster (MazeBuffType == "Character") and AddBuff any that aren't
  - already on the caster's BuffList. Existing dedup logic in
  - AvatarSceneInfo.AddBuff prevents double-adding for characters whose
  - ability JSON already calls AddMazeBuff. Also load MazeBuffType into
  - MazeBuffExcel so this filter is data-driven.

## Merge pull request #6 from Mar7thLover/fix/moc-stage2-and-dahlia-technique

- Contributor: Mar7thLover (Cyrene)
- Commit: `442880bff4d9388131f3eef3cc1f39cad7cc6e44`
- Description:
  - fix: MoC stage-2 entry and Dahlia technique buff backfill

## fix(skill): scope technique maze-buff backfill to the canonical SkillMaze entry

- Contributor: March7thHoney
- Commit: `1ceca26d6acbab21c4c6d2eec3b716ce3ac242a2`
- Description:
  - The cd1316fb backfill scanned every Character-typed AvatarMazeBuff entry
  - matching the avatar id, which freezes Mar7thLover (Cyrene) (1415) and Cerydra (1412) in
  - the overworld: their AvatarMazeBuff tables expose multiple entries
  - (Mar7thLover (Cyrene) 141501/141502/141503, Cerydra 141201/141202/141203) and the
  - overly broad filter pushed the summon-unit / listener / empty buffs onto
  - the caster. Official packets only deliver the primary maze buff.
  - Filter candidates by InBattleBindingType=CharacterSkill +
  - InBattleBindingKey=SkillMaze and load those columns into MazeBuffExcel.
  - If the ability JSON already AddMazeBuff'd one of the candidates (Mar7thLover (Cyrene)'s
  - 141501) skip the whole backfill; otherwise apply the single entry whose
  - UseType is SummonUnit/AddBattleBuff/TriggerBattle, preserving the
  - original Dahlia 132101 / Sunday 131301 / Robin 130902 fix.

## fix(gacha): map Banners.json id field to BannerConfig.GachaId

- Contributor: March7thHoney
- Commit: `476aded563d807352353f95a0d7f8ca59310ef4b`
- Description:
  - Banners.json uses "id" but BannerConfig declared GachaId without a
  - JsonProperty attribute, so every banner deserialized with GachaId=0.
  - This stripped gacha_id (and gachaCeiling for the standard banner) from
  - GetGachaInfoScRsp and broke DoGacha banner lookup, preventing the gacha
  - UI from opening.

## Merge pull request #9 from Mar7thLover/fix/technique-maze-buff-and-gacha-id

- Contributor: Mar7thLover (Cyrene)
- Commit: `62d35659b231e90c4ba64aec8f7e4fc8b8b53e10`
- Description:
  - fix: 秘技战斗外卡死 + 抽卡 banner id 反序列化

## Fix open chest

- Contributor: Mar7thLover (Cyrene)
- Commit: `e53c8ea814ce621908f7bffbaca759ee61f87445`
- Description:
  - (No additional commit description)

## Add QueryProductInfoConfig and MonthCard config.

- Contributor: Mar7thLover (Cyrene)
- Commit: `6c0c1382bf5795f4d911840bd348733b03c0e295`
- Description:
  - Basic implement Gril Type
  - TODO:Add Boy type support

## fix: lineup stability and exchange/monthcard flow

- Contributor: Mar7thLover (Cyrene)
- Commit: `80c9c8ffd09e9d4ccddb93db52e7a31dcc8f4241`
- Description:
  - - fix ReplaceLineup/ToProto null-add crash by hardening lineup avatar serialization and normalizing multi-path avatar ids to base avatar ids in lineup writes
  - - implement ExchangeHcoin handler and response packet with actual currency sync
  - - make MonthCard login reward actually grant 90 Hcoin before notify
  - - include current resource cache snapshot as requested
  - Known issue:
  - - attempts to fix destructible prop drop rewards are still not successful in all cases; further investigation is required

## feat: implement TrainCakeCatch (列车改造奇遇 cat tree placement)

- Contributor: March7thHoney
- Commit: `0b0f00a1c04ba625d9140fddc988cf0c9c6dcfc5`
- Description:
  - - Add CakeConfigExcel and CakeCatchConstValueCommonExcel to load 27
  -   creature configs and gameplay constants from existing JSON resources
  - - Add TrainCakeCatchData SqlSugar table to persist cat tree placements,
  -   performance Diy state, and unlocked performance ids
  - - Add TrainCakeCatchManager that builds GetDataScRsp with all 27
  -   creatures unlocked at max quantity and round-trips Diy mutations
  - - Add 5 packet handlers (GetData/Diy/Search/OpenBox/DiyLike) and 2 send
  -   packet wrappers, kept in dedicated TrainCakeCatch namespace
  - - Wire TrainCakeCatchManager into PlayerInstance

## feat: unlock all 12 cake performance observation records

- Contributor: March7thHoney
- Commit: `bbefaeae43b5f899a5436217a34a174b871ec752`
- Description:
  - - Add CakePerformanceConfigExcel to load 12 entries from
  -   Resources/ExcelOutput/CakePerformanceConfig.json
  - - Populate TrainCakeCatchGetDataScRsp.PerformanceIdList by unioning all
  -   config keys with the player's persisted PerformanceIds, so every
  -   observation record is shown unlocked while preserving the persisted
  -   field for future per-player tracking

## feat: auto-finish all 12 cake performance quests on activity entry

- Contributor: March7thHoney
- Commit: `a270dbc1b0da6fd68c8a879500c60c6dfa416d98`
- Description:
  - - Extend CakePerformanceConfigExcel with QuestID field
  - - Add TrainCakeCatchManager.UnlockAllPerformanceQuests that directly
  -   marks each linked quest as QuestFinish, dirties the save list, and
  -   pushes a PlayerSyncScNotify, bypassing the standard accept-then-finish
  -   path so it does not trigger 12 full AcceptQuestByCondition scans
  - - Call the unlock from HandlerTrainCakeCatchGetDataCsReq before sending
  -   the GetData response so observation-record rewards are claimable

## Merge pull request #10 from Mar7thLover/feat/train-cake-catch

- Contributor: Mar7thLover (Cyrene)
- Commit: `22ab9e354889d0e87874d960ee0ae9667e79e6b6`
- Description:
  - feat: implement TrainCakeCatch activity (列车改造奇遇)

## Better ResourceManager

- Contributor: Mar7thLover (Cyrene)
- Commit: `f59367a5545eb8b8a114fdf1cf1bf4948561c3b1`
- Description:
  - (No additional commit description)

## Better ResourceManager

- Contributor: Mar7thLover (Cyrene)
- Commit: `da6415590f008ff4cfe01f199404b2b2cf381423`
- Description:
  - (No additional commit description)

## feat(expedition): implement expedition flow, persistence, and accept-based start

- Contributor: Mar7thLover (Cyrene)
- Commit: `a61d96ac927d1a8342f3b26f1695bf25957d35b4`
- Description:
  - - add expedition excel loader (ExpeditionData) and GameData mapping
  - - add ExpeditionManager and register in PlayerInstance
  - - implement GetExpeditionDataCsReq/ScRsp pipeline
  - - implement AcceptMultipleExpeditionCsReq/ScRsp pipeline
  - - require Accept before starting expedition flow (no default active expeditions)
  - - persist active expedition ids in database table Expedition
  - - set refresh time to accept_time + 5 minutes and persist it
  - - fix expedition capacity display by reporting totalExpeditionCount as 4
  - - include updated resource cache

## fix(scene): correct destructible prop flow and speed-buff handling

- Contributor: Mar7thLover (Cyrene)
- Commit: `c5ea1f8d37e47a28afd83ea2d30d3ce742cce177`
- Description:
  - - process destructible hits even when attackedByEntityId is missing
  - - remove destructed props via deleteEntity refresh instead of addEntity open-state refresh
  - - apply speed buff (2041101, 15s) for event 10003 destructibles
  - - always send ScenePlaneEventScNotify for destruct event, including empty getItemList
  - - prevent opened destructibles from respawning on scene reload

## fix(expedition,scene): implement expedition reward take flow and improve summon/buff sync

- Contributor: Mar7thLover (Cyrene)
- Commit: `ef675c327929a73c29e94c2a12ce913705a8e4a0`
- Description:
  - - implement TakeMultipleExpeditionReward request/response pipeline
  - - load ExpeditionReward excel and build reward items from config
  - - enforce expedition reward cooldown checks and persist next refresh
  - - improve summon-related buff sync fields (castEntityId/reason/summon reference)
  - - align summon refresh type for scene group updates
  - Note: 秘技实体召唤问题尚未完全解决，仍需继续对齐官服时序与字段细节。

## refactor(gridfight): split sync/battle modules and fix enter-battle flow

- Contributor: Mar7thLover (Cyrene)
- Commit: `1bf6612ebca6a986f797b31cd4450122ffc02b8f`
- Description:
  - - extract GridFight battle startup into dedicated module
  - - move pending-action handling into processor and add sync builders
  - - move GridFightSyncKind to Game/GridFight/Sync as non-enum constants
  - - keep protocol compatibility while reducing handler logic coupling

## Merge pull request #11 from Mar7thLover/feat/gridfight-sync-battle-module

- Contributor: Mar7thLover (Cyrene)
- Commit: `2270ee2f98c3fb1d261d5455b540865259dba1a9`
- Description:
  - refactor(gridfight): split sync/battle modules and fix enter-battle flow

## fix(gridfight): use node lineup for battle instead of world lineup

- Contributor: March7thHoney
- Commit: `b70e1b3424a6018d123a8e3081b01e5d0993cae1`
- Description:
  - - Add GridFightContext on BattleInstance and route ToProto through GridFightBattleProtoBuilder when set
  - - StartBattle now builds a temporary LineupChessRogue lineup from node foreground/background avatars (replacing LineupManager.GetCurLineup)
  - - Split BuildBattleAvatarIds into BuildForegroundAvatarIds (pos 1-4) and BuildBackgroundAvatarIds (pos 5-13)
  - - Allow GridGameRoleList in EnterBattleStageScRsp to include the full board (pos 1-13) instead of only foreground

## Merge pull request #12 from Mar7thLover/fix/gridfight-node-lineup-battle

- Contributor: Mar7thLover (Cyrene)
- Commit: `0b75d060bf18fd8a4136bebabee89b190a4c8f3e`
- Description:
  - fix(gridfight): use node lineup for battle instead of world lineup

## feat(web): integrate RSA login decrypt with security key files

- Contributor: Mar7thLover (Cyrene)
- Commit: `b707e34f13015caa1afa4fc8033797a6cbd1a400`
- Description:
  - (No additional commit description)

## chore(research): update patched AccountPlatNative.dll public keys

- Contributor: Mar7thLover (Cyrene)
- Commit: `ac891271e5af82cb7716441b346cf24be9b3fdf9`
- Description:
  - (No additional commit description)

## refactor(trainparty): replace legacy path with code-built train party payload

- Contributor: Mar7thLover (Cyrene)
- Commit: `6545b502a42d9db9c25cbbc968e8b7defaa63445`
- Description:
  - (No additional commit description)

## Basic Implement TrainParty

- Contributor: Mar7thLover (Cyrene)
- Commit: `b5f7aebeff19b2187904aed69feb2954e04acf1c`
- Description:
  - (No additional commit description)

## Chore:add some i18n text

- Contributor: Mar7thLover (Cyrene)
- Commit: `9ad8d3b8fbc199d960ad291de4c478247677871b`
- Description:
  - (No additional commit description)

## Add TrialDays

- Contributor: Mar7thLover (Cyrene)
- Commit: `a80a3204f73d1e84b4b26777fdab7a9236c915a9`
- Description:
  - (No additional commit description)

## Single Publish fix

- Contributor: Mar7thLover (Cyrene)
- Commit: `27d682a0df3c1a255a6d378bbbbf2c13bb0d0d3f`
- Description:
  - (No additional commit description)

## Better Script

- Contributor: Mar7thLover (Cyrene)
- Commit: `22f9920ab03328d5b5947f7b910d8b50ad837a4c`
- Description:
  - (No additional commit description)

## fix(gridfight): render battle as GridFight form with trial avatars and proper front/back roles

- Contributor: March7thHoney
- Commit: `cfba033468ba72cdb85600874a967b749a7b82d7`
- Description:
  - - Force stageId=70000001 (GridFightActivity template) instead of generic StageCandidates
  - - BattleAvatarList: foreground roles (pos 1-4) use SpecialAvatarID as trial avatars (AvatarTrialType)
  - - AFCMOOFGBPK.PIDIGFGKAMK: background roles (pos 5-13) override Id to baseAvatarId with AvatarGridFightType
  - - Add GridFightLevelResolver: drives wave/eliteGroup/buff/event/trait from GridFightStageRoute + NodeTemplate + RoleBasicInfo
  - - Add FrontBackType/EquipmentID to GridFightRoleBasicInfoExcel; route by pos (1-4 front / 5-13 back) since any role can be placed anywhere
  - - ApplyPositionList clears stale uniqueId mappings so a role moved out of bench doesn't keep its old pos
  - - AvatarManager.GetTrialAvatarByWorldLevel: resolve SpecialAvatar by SpecialAvatarID*10+worldLevel with fallback
  - - Inject _BindingMazeBuff (35100001) + battle events with maxSp=100000 to satisfy GridFight stage template

## fix(gridfight): trait recognition and battle end packet sequence

- Contributor: March7thHoney
- Commit: `18c8e4e2fe851571bc5d801917326234b0577989`
- Description:
  - - Add GridFightInstance.CheckTrait(): counts trait members from on-board roles, resolves layer via GridFightTraitLayer threshold table, fills MEEPFKLLIJB members with uniqueId, populates NKFDBEHPNLG (TraitEffectLayer) and TraitEffectList when activated
  - - EnterBattleStageScRsp now uses CheckTrait so the client recognizes activated traits (e.g. Asta+Sampo = 2 Continuous Damage)
  - - Fix BuildEndBattleNotify field values: KDOINLGKMBI/IJEIEJLPGGJ use LineupMaxHp instead of BattleMaxHp; BCOLJFHDLLD/FGEDKOINMAG use LineupHp; EJEIBPEKHLD=3
  - - EndBattle no longer sends PacketGridFightHandlePendingActionScRsp / SyncKeepWinCntNotify / SeasonHandBookNotify (not present in official capture, caused client to wait for matching CsReq)
  - - Move OnBattleResolved (SectionId++) to after EndBattleStageNotify so the notify carries the just-resolved sectionId

## fix(challenge): unlock cumulative star rewards for all endgame modes

- Contributor: March7thHoney
- Commit: `a04f9735fa019f300a7928fa308baee9f255c4da`
- Description:
  - - Load ChallengeBossRewardLine.json so RewardLineGroupID=3000 resolves for Apocalyptic Shadow groups
  - - Count totalStars by iterating ChallengeConfigData per group (3 stars for stages absent from History), aligning with the Star=7 fallback PacketGetChallengeScRsp sends to the client

## fix(challenge): include Hcoin (stellar jade) in cumulative star rewards

- Contributor: March7thHoney
- Commit: `2d290b893fd4b628ee6d4fc84ab9db41a13cd1d9`
- Description:
  - RewardData rows for endgame star tiers carry Hcoin in a dedicated column,
  - not in the ItemID_x slots that GetItems() returns. Mirror the pattern used
  - by ExpeditionManager/MissionManager and prepend Hcoin as ItemId=1 before
  - the explicit item slots.

## fix(challengepeak): load peak reward table and evaluate by RewardType

- Contributor: March7thHoney
- Commit: `61c041bb318ce8a6cbcae8be6e2b6c6893877e9d`
- Description:
  - ChallengePeakReward.json was never loaded, so TakeRewards fell back to
  - ChallengeMazeRewardLine entries that share GroupID=1 and silently rejected
  - every claim. Add ChallengePeakRewardExcel to load it into a new
  - GameData.ChallengePeakRewardData bucket and rewrite TakeRewards to:
  - - match the client's normalRewardIdList against the peak reward entry ID
  - - dispatch on RewardType (MOB_PASS / MOB_STAR / BOSS_STAR / BOSS_STAR_LIMIT
  -   / BOSS_COLOR_TARGET) using the same 'all stages cleared 3-star' assumption
  -   that GetChallengePeakInfo already reports to the client
  - - include Hcoin (stellar jade) alongside the explicit item slots

## Merge pull request #13 from Mar7thLover/fix/gridfight-battle-render

- Contributor: Mar7thLover (Cyrene)
- Commit: `4825a3e87ec1625dec20a5be0ce57d0b23bbfc16`
- Description:
  - fix(gridfight,challenge): enter battle properly + cumulative star rewards claimable

## Create dotnet-desktop.yml

- Contributor: Mar7thLover (Cyrene)
- Commit: `2e6d89f5d86ed1373146bff9e25ffc31622454fa`
- Description:
  - (No additional commit description)

## Refactor license trial flow and add cross-platform HWID support

- Contributor: Mar7thLover (Cyrene)
- Commit: `4f8dde0823543406b24144329cbc5c72f6322ebd`
- Description:
  - (No additional commit description)

## Better workflow

- Contributor: Mar7thLover (Cyrene)
- Commit: `875ccabef03af282fb87bcedf94c5ab7a1d5b792`
- Description:
  - (No additional commit description)

## Refactor WebServer login handlers and stabilize token verification flow

- Contributor: Mar7thLover (Cyrene)
- Commit: `6131c5341e6baba7f99121b3371e9bb0d1646bdd`
- Description:
  - (No additional commit description)

## fix(proto): align SceneSummonUnitInfo field ids with LC

- Contributor: Mar7thLover (Cyrene)
- Commit: `ffcbd69339d6946121e6f8b63706e30eb2f1d27c`
- Description:
  - (No additional commit description)

## fix(scene): stabilize summon lifecycle and set 45s summon duration

- Contributor: Mar7thLover (Cyrene)
- Commit: `8aaacb0fecb7e109f67c0e387e71242ec6e3fb9f`
- Description:
  - (No additional commit description)

## feat(webserver): add RSA fallback decode for admin auth and command verify

- Contributor: Mar7thLover (Cyrene)
- Commit: `98d97284a63627a979331c3c35c574c8fccb1f5e`
- Description:
  - (No additional commit description)

## feat(common): add configurable fake server time provider

- Contributor: Mar7thLover (Cyrene)
- Commit: `0b85c5bd1fd82f9387e450fa815b98f7493236b1`
- Description:
  - (No additional commit description)

## feat(quest): implement optional reward handler and response payload

- Contributor: Mar7thLover (Cyrene)
- Commit: `7423997dddf112cc37841b24dd2392db38883353`
- Description:
  - (No additional commit description)

## feat(activity): add TakeReward handler with fixed reward response

- Contributor: Mar7thLover (Cyrene)
- Commit: `b86f042a776906363d2b68461408d997d1541be6`
- Description:
  - (No additional commit description)

## feat(item): deduct equivalent material when exchanging hcoin

- Contributor: Mar7thLover (Cyrene)
- Commit: `4d3b56a4eacd565aa6d232dae10e641660632bcd`
- Description:
  - (No additional commit description)

## feat:The Correct Logic of Exchange

- Contributor: Mar7thLover (Cyrene)
- Commit: `f8a2b3ae6489dd688c414ed65856949baddcb9a4`
- Description:
  - (No additional commit description)

## Removed un-used logic

- Contributor: Mar7thLover (Cyrene)
- Commit: `f1e294bb168a5e234b630c336091a0c502e79add`
- Description:
  - (No additional commit description)

## refactor(resource): summarize missing-file output and log full missing paths

- Contributor: Mar7thLover (Cyrene)
- Commit: `cbf464815e8730462041a8955ad0d208af60a290`
- Description:
  - (No additional commit description)

## feat:implement main mission

- Contributor: Mar7thLover (Cyrene)
- Commit: `541cd22dd1f30fd999efc808e91a103d1272d8a0`
- Description:
  - (No additional commit description)

## Update to .NET10 SDK

- Contributor: Mar7thLover (Cyrene)
- Commit: `f5aa43e0f68c8535e28e4bd081e0a9e78880e4d8`
- Description:
  - (No additional commit description)

## Update workflow to .NET10

- Contributor: Mar7thLover (Cyrene)
- Commit: `840e7c4f90b6905bc43b4c47b818995f88187394`
- Description:
  - (No additional commit description)

## fix(activity): expose activity entries on activity-bound floors

- Contributor: March7thHoney
- Commit: `fc7e3aac0db15461cf148d673be7ae9459c41f75`
- Description:
  - - Extend ContentPackageConfigExcel with MainMissionIDList,
  -   ActivityModuleID, InitEntranceID, AfterGuideEntranceID, and add
  -   TouchesFloor(floorId) helper that resolves entrances via
  -   MapEntranceData.
  - - MissionManager.OnLoadScene: for each ContentPackage whose entrance
  -   resolves to the current floor, surface its MainMissionIDList in
  -   SceneMissionInfo.UnfinishedMainMissionIdList so the client treats
  -   the activity as live. Does not mutate player mission state, so it
  -   cooperates with the recently revamped Mission system instead of
  -   fighting it.
  - - SceneInstance.ToProto: on activity-bound floors, raise floor saved
  -   values that default to 0 (and are not *_IsHidden) to 1, so new
  -   accounts also unlock activity NPC map icons and entrances.

## feat(activity): implement DiceCombat activity main page and battle flow

- Contributor: March7thHoney
- Commit: `23a0ff2bd29a6fd38187fb53936262ae5c75cf58`
- Description:
  - - Add DiceCombatActivityInstance and config to hold avatars/stages/unlocks state
  - - Register the instance on ActivityManager construction
  - - Add Recv/Send handlers for GetSystemData, GetShopData, MainPageRollDice,
  -   FinishPveStage, ModifyAvatarDice, UpgradeAvatar, BuyShopGood
  - - Populate GetDiceCombatSystemDataScRsp with default unlocked avatars, completed
  -   stages and score progress so the client can render the main panel

## fix(activity): respond to V2FinishPveStage to unblock DiceCombat KO

- Contributor: March7thHoney
- Commit: `81386d85f4da3ebf324da0b93cdd3bc495163652`
- Description:
  - - Add HandlerV2FinishPveStageCsReq under DiceCombat so the dispatcher no
  -   longer falls back to an empty V2FinishPveStageScRsp after a KO
  - - Echo is_win and stage_id on the response and record the stage as
  -   completed on the activity instance

## fix(activity): seed DiceCombat state from official completed snapshot

- Contributor: March7thHoney
- Commit: `adae0aee707f0b511ab9afc25abe1b4374545142`
- Description:
  - - Replace hand-rolled avatar/stage defaults with the captured 1409-byte
  -   GetDiceCombatSystemDataScRsp payload from an official server account
  -   that completed the activity
  - - Parse the snapshot in DiceCombatActivityInstance so per-avatar level,
  -   dice_id_list and unlock_time match the official client expectations,
  -   unblocking the in-battle deploy ('上阵') flow
  - - Route ModifyAvatarDice / UpgradeAvatar through the snapshot via
  -   FindAvatar so responses echo the same avatar shape

## fix(activity): grant Combat Analyst Certificate to unlock DiceCombat dice swap

- Contributor: March7thHoney
- Commit: `6ba07a94b2e57903d75865f8cfb24a18a4e350b5`
- Description:
  - - Hand out item 150093 (战力分析家证书) on the first DiceCombat system
  -   data request when it's not already in inventory
  - - The client gates the 更换曜彩骰 button on owning this item, so without
  -   it the action was blocked locally with no packet ever reaching the server

## Merge pull request #14 from Mar7thLover/feat/dice-combat-activity

- Contributor: Mar7thLover (Cyrene)
- Commit: `4eb0f416b5b9047d79b068bceabc125984794c12`
- Description:
  - Fix activity entries on activity floors and implement DiceCombat

## Fix Update Database

- Contributor: Mar7thLover (Cyrene)
- Commit: `fd5fb3fa16122f86581f0e4a07bb43a73543b7c8`
- Description:
  - (No additional commit description)

## fix:No longer automatically sets challenges to full stars.

- Contributor: Mar7thLover (Cyrene)
- Commit: `036238b97f49ae4d540da0f80682eb6634133a34`
- Description:
  - (No additional commit description)

## refactor(dice-combat): replace base64 snapshot with code-built equivalent payload

- Contributor: Mar7thLover (Cyrene)
- Commit: `7d8d62b61aa01e8bf5efb9f0dba34a3f469b305d`
- Description:
  - (No additional commit description)

## fix(trial-activity): load only stage group and relax monster group gate

- Contributor: Mar7thLover (Cyrene)
- Commit: `3800446005b4b32ec51d7a9a72cf5bf00a02b668`
- Description:
  - (No additional commit description)

## fix(scene): use AFIBFMAFNCC refresh type for default delete-entity updates

- Contributor: Mar7thLover (Cyrene)
- Commit: `b7e9c18f505a37967d46d042f9eb31b1f817d34c`
- Description:
  - (No additional commit description)

## fix:show rateupstar4 item

- Contributor: Mar7thLover (Cyrene)
- Commit: `ebedbae9a489db8d333072ebf304eec500cae681`
- Description:
  - (No additional commit description)

## Fix TakeQuestReward won't refresh correctly

- Contributor: Mar7thLover (Cyrene)
- Commit: `035a26d17dc893fba95cc7ad80629787c8a69142`
- Description:
  - (No additional commit description)

## fix(resource): load AdventurePlayerLD so Saber/Archer skill cast resolves

- Contributor: March7thHoney
- Commit: `34e9aa067c76044ba9742d9cd74346202f2daa41`
- Description:
  - AdventurePlayerExcel only declared AdventurePlayer.json and
  - ActivityAdventurePlayer.json in its ResourceEntity, missing
  - AdventurePlayerLD.json where collab characters Saber (1014) and
  - Archer (1015) live. Without those entries LoadMazeSkill never
  - registered their Ability JSON into AdventureAbilityConfigListData,
  - so SceneSkillManager.OnCast returned RetMazeNoAbility (2204) and
  - the client popped "没有选择增益效果" on every normal attack and
  - technique cast. Add AdventurePlayerLD.json to the entity list and
  - refresh Resource.cache so the rebuilt snapshot picks the new
  - entries up on next boot.

## Merge pull request #15 from Mar7thLover/fix/saber-archer-skill-popup

- Contributor: Mar7thLover (Cyrene)
- Commit: `6bc10798efd34f3c7ee13f22ef7e29d6437eb34b`
- Description:
  - fix: 修复 Saber / Archer 在场景中攻击和释放秘技弹出"没有选择增益效果"

## refactor(resource): replace hardcoded ResourceManager logs with i18n

- Contributor: Mar7thLover (Cyrene)
- Commit: `c0ed0e17979ced51a368fa27559be5f9a48b8869`
- Description:
  - (No additional commit description)

## fix(avatar): validate and return proper take promotion reward response

- Contributor: Mar7thLover (Cyrene)
- Commit: `9bac64e276ff258ab3eb16a873fce8f5f321776b`
- Description:
  - (No additional commit description)

## feat(inventory): update relic composition to handle multiple items and adjust inventory limits

- Contributor: Meliodas
- Commit: `72d4e0b581e762e6e117b6a90b23a40e46cf833d`
- Description:
  - (No additional commit description)

## fix(unlock): make /giveall unlock cover avatar skins and notify clients live

- Contributor: March7thHoney
- Commit: `bd4ee6b1cf5b72098d9652c84e142299105e7653`
- Description:
  - - Include ItemConfigAvatarSkin.json in ItemConfigExcel loader so 5 skin
  -   items (Mar7th/Ruan Mei/Firefly/Castorice/Trailblazer) enter ItemConfigData
  -   and the AvatarSkin branch in InventoryManager.AddItem fires.
  - - Add PacketUnlockChatBubbleScNotify / PacketUnlockPhoneThemeScNotify
  -   wrappers around the existing proto messages (5114 / 5198).
  - - InventoryManager.AddItem: dedupe HeadIcon / ChatBubble / PhoneTheme /
  -   PersonalCard / PhoneCase / AvatarSkin adds via Contains checks, and emit
  -   the corresponding unlock notify for ChatBubble / PhoneTheme / AvatarSkin
  -   so the client refreshes without relog. Guard AvatarSkin lookup with
  -   TryGetValue to avoid KeyNotFoundException on unknown ids.

## chore(cache): regenerate Resource.cache after ItemConfigAvatarSkin load fix

- Contributor: March7thHoney
- Commit: `a037923a2f4c102c8a1e0407c098d75d4c3cbad9`
- Description:
  - The previous cache was generated before ItemConfigAvatarSkin.json joined
  - the ItemConfigExcel loader list, so it omitted the 5 AvatarSkin items.
  - Resource.cache short-circuits LoadGameData on startup, which kept the old
  - data alive across rebuilds and was the real reason /giveall unlock could
  - not grant skins. This commit checks in the freshly regenerated cache so
  - other developers do not need to delete it manually.

## feat(unlock): grant Trailblazer player outfits via /giveall unlock

- Contributor: March7thHoney
- Commit: `11c27bcdfdae08dc2d1dd43d16f1108d3a24898a`
- Description:
  - - Add ItemSubTypeEnum.PlayerOutfit to GiveAllUnlock whitelist so 227xxx
  -   trailblazer outfit items (caps, glasses, costumes) are iterated.
  - - InventoryManager.AddItem: new PlayerOutfit case dedupes and writes
  -   Player.Data.PlayerOutfitList, then emits PlayerSyncScNotify carrying
  -   the refreshed PlayerOutfitInfo so the avatar panel updates live.
  - - PhoneCase branch now also fires UnlockPhoneCaseScNotify (CmdId 5139)
  -   via a new PacketUnlockPhoneCaseScNotify wrapper, matching the live
  -   refresh story already in place for ChatBubble / PhoneTheme.

## fix(phone): swap owned-themes / owned-chat-bubbles to match retail proto

- Contributor: March7thHoney
- Commit: `0d61fb68dbaeec585a102bd755bbeeafc7800736`
- Description:
  - Capture from the retail server shows GetPhoneDataScRsp packs the phone
  - theme list into the field named OwnedChatBubbles and the chat bubble
  - list into OwnedPhoneThemes (the reverse-engineered proto descriptor
  - mislabeled the two repeated fields; their tag numbers carry the opposite
  - meaning on the client). Until the field labels are corrected we mirror
  - retail by flipping the population direction on the server side, which
  - restores the wallpaper sub-tab in the phone decoration UI.
  - The Cur* singular fields and OwnedPhoneCases are unaffected.

## feat(outfit): implement SetPlayerOutfit handler and persist unlock state

- Contributor: March7thHoney
- Commit: `07df6b38e8d48466247e5f2070da12b19a9ec3d9`
- Description:
  - - Add HandlerSetPlayerOutfitCsReq (cmd 302) and PacketSetPlayerOutfitScRsp
  -   (cmd 349). The handler mirrors retail behavior captured from the live
  -   server: overwrite Data.PlayerOutfitList with the OutfitIDs the client
  -   posts, broadcast PlayerSyncScNotify carrying the refreshed
  -   PlayerOutfitInfo, and reply with an empty SetPlayerOutfitScRsp.
  -   Previously the request had no handler, so the optimistic "更换成功"
  -   toast on the client never matched real state and the avatar reverted on
  -   reopen.
  - - InventoryManager.AddItem PlayerOutfit branch now routes through
  -   PutItem so 227xxx items live inside InventoryData.MaterialItems, which
  -   is how the retail GetBagScRsp packs them. The previous attempt stuffed
  -   ItemIDs into Data.PlayerOutfitList, but that list is the equipped
  -   OutfitID set, not the owned list.
  - - Mark the player dirty (ToSaveUidList.SafeAdd) inside every unlock-list
  -   mutation. Without this the new entries lived only in memory until the
  -   5-minute periodic flush, so disconnecting before the next save dropped
  -   the unlocks on reconnect.

## fix(skill): trigger battle on technique hit by defaulting AdventureTriggerAttack.TriggerBattle to true

- Contributor: March7thHoney
- Commit: `d759ac475f32bbd0e361946124321edd483f4ca2`
- Description:
  - Archer's MazeSkill (and any future projectile-attack technique) failed to enter
  - battle because its ability JSON omits the TriggerBattle field, while the C# model
  - defaulted to false. Official semantics treat the absent field as true; characters
  - that should not enter battle (Anaxa, Argenti, Dr_Ratio, ...) explicitly set
  - TriggerBattle: false, which still suppresses correctly after this change.
  - Regenerated Config/Resource.cache so the cached AdventureAbility data reflects
  - the new default.

## Merge pull request #16 from Mar7thLover/fix/unlock-outfit-archer

- Contributor: Mar7thLover (Cyrene)
- Commit: `bcaece287ae6da35735cfe2bab8c1db208ed87b4`
- Description:
  - fix: /giveall unlock + 皮肤/开拓穿搭/手机壁纸 + Archer 秘技进战

## feat(mail): Implement mail attachment handling and welcome mail feature

- Contributor: Meliodas
- Commit: `221f1e97b52859a374ed0237eafab57a0b75c850`
- Description:
  - Configurable through config.json

## fix(avatar): heal legacy enhanced-prefix PointIDs in unenhanced skill tree

- Contributor: March7thHoney
- Commit: `ca6e46dd14463d76bdd3bd351f23ac963c325f22`
- Description:
  - Welt/Blade (and any future enhanceable avatar) cached enhanced-prefix
  - PointIDs (e.g. 11004XXX) inside EnhanceInfos[0] from older /talent runs.
  - When switching back to the unenhanced path the BattleAvatar proto leaked
  - those PointIDs with enhancedId=0, so the client stalled and never entered
  - battle.
  - GetSkillTree now remaps any cached entry whose AvatarSkillTreeConfig
  - EnhancedID disagrees with the active EnhanceId, matching by AnchorType
  - slot to preserve the user's leveled values under the correct PointIDs.

## fix(avatar): load LD item configs so collab avatars (saber/archer) work with /giveall and /json

- Contributor: March7thHoney
- Commit: `8189d994b75f2024a78b1bed7153b98e847a3b93`
- Description:
  - - Add ItemConfigAvatarLD.json, ItemConfigAvatarRankLD.json and
  -   ItemConfigAvatarPlayerIcLD.json to ItemConfigExcel's ResourceEntity
  -   list. Without these, ItemConfigData lacked entries for 1014/1015,
  -   causing InventoryManager.AddItem to silently return null and both
  -   /giveall avatar and /json to skip saber and archer. /avatar get
  -   worked because it bypasses ItemConfigData via AvatarManager.AddAvatar.
  - - Regenerate Config/Resource.cache so the new LD entries are present
  -   (the previous cache was loaded on startup, masking the fix).

## fix(player): default new account HeadIcon to female trailblazer (208002) to match default CurBasicType

- Contributor: March7thHoney
- Commit: `71b7f36243104ba272723ece80887edba7bb2274`
- Description:
  - (No additional commit description)

## fix(farm-element): restore player position and preserve entity after stagnant shadow battle

- Contributor: March7thHoney
- Commit: `8318879b30ce431e526ccc292d2de9e646465068`
- Description:
  - - Track FarmElement entityId and player pos/rot on ActiveFarmElementCsReq
  - - Clear FarmElement state on DeactivateFarmElementCsReq
  - - Echo request worldLevel in ActiveFarmElementScRsp instead of player.Data.WorldLevel
  - - Add removeFromScene param to EntityMonster.Kill; skip RemoveEntity/IsAlive flip when keeping
  - - BattleManager.EndBattle: keep FarmElement monster in scene, MoveTo saved pos on win
  - Fixes character getting stuck inside the shadow and the challenge being un-interactable after clearing it.

## feat(farm-element): handle ReEnterLastElementStageCsReq for stagnant shadow retry

- Contributor: March7thHoney
- Commit: `4a071843bc5274af06f1557a6301a9e6a072889b`
- Description:
  - - Add FarmElementConfigExcel loader for FarmElementConfig.json indexed by StageID
  - - Register FarmElementConfigData in GameData
  - - Add BattleManager.StartFarmElementStage(config) to spin up a single-stage battle with stamina cost
  - - Add HandlerReEnterLastElementStageCsReq + PacketReEnterLastElementStageScRsp to support the in-result "再来一次" button
  - - Regenerate Config/Resource.cache so the new GameData field is populated on warm boot
  - Without this, clicking "再来一次" on a stagnant shadow result screen produced a fallback empty response and "消息获取失败".

## 花萼模式初步实现（/calyx on 1 开启花萼模式，加载第一个freesr

- Contributor: March7thHoney
- Commit: `9f5819afc8d1fca6753bddd3721e8ae0ea6856ea`
- Description:
  - /calyx off — 关闭花萼模式）

## ci: publish win-x64 and linux artifacts with unified naming

- Contributor: Mar7thLover (Cyrene)
- Commit: `dfd296010333e7e478402efbd23dc5bbfec4b4e0`
- Description:
  - (No additional commit description)

## Fix enhanced when load freesrdata

- Contributor: Mar7thLover (Cyrene)
- Commit: `3ef3571222daeb441ab57b8ddeeb94c393291e3e`
- Description:
  - (No additional commit description)

## fix mission review exit

- Contributor: Yuki.
- Commit: `1cec70ff70e34065031544d36002cfd3f6320b86`
- Description:
  - OMG

## Merge pull request #19 from Mar7thLover/fix/mission-review-exit

- Contributor: Mar7thLover (Cyrene)
- Commit: `348c4782bdc52275dc00514cf7bcecf620d6bb90`
- Description:
  - fix mission review exit

## workflow

- Contributor: Mar7thLover (Cyrene)
- Commit: `9c3f4f0ec9ee07e01600a0167728ee4d818a65d8`
- Description:
  - (No additional commit description)

## Container

- Contributor: Mar7thLover (Cyrene)
- Commit: `b07e7e68cd23a65d12aeedea4405b20a2ed4372c`
- Description:
  - (No additional commit description)

## docs: refresh README and localized docs with enhanced structure

- Contributor: Mar7thLover (Cyrene)
- Commit: `3e9ef5d7bfc10c688f5f5fd625e4d78f2c219065`
- Description:
  - (No additional commit description)

## Better ReadMe

- Contributor: Mar7thLover (Cyrene)
- Commit: `fb99e1dcc9b637dd4e82032666ecf2d3de4a5e45`
- Description:
  - (No additional commit description)

## fix(calyx): treat freesr techniques as maze buff id list and tag with SkillIndex

- Contributor: March7thHoney
- Commit: `682f8d37652127ee472c05ec172f4c89b691844a`
- Description:
  - - Drop the brittle MazeBuffData binding/UseType heuristics that mis-picked
  -   the wrong buff for technique-only characters (e.g. Castorice 140702 vs
  -   the correct 140701, Mar7thLover (Cyrene) 141501 vs the debuff 141502).
  - - Iterate freesr `techniques` array directly: every entry is the buff id
  -   to inject for that avatar. Fully data-driven, no per-character knowledge.
  - - Only inject for avatars actually in the current battle lineup.
  - - Add `SkillIndex=0` dynamic value on each injected buff so the client
  -   recognizes it as technique-triggered (matches M7thLite convention) —
  -   fixes Castorice summoning Death's Dragon failing to render.

## feat(calyx): inject endgame battle targets and maze buff when overriding into PF/AS stages

- Contributor: March7thHoney
- Commit: `b3b9a78f250f6264eae7aeb59667e1c2ca4b107c`
- Description:
  - - Build a lazy stage_id -> ChallengeConfigExcel reverse index from
  -   ChallengeMonsters EventId (with PlaneEventData mapping) so any freesr
  -   stage that points at a real Pure Fiction / Apocalyptic Shadow / Memory
  -   of Chaos config is detected without hardcoding stage_id ranges.
  - - For matched stages, mirror ChallengeStoryInstance/ChallengeBossInstance
  -   OnBattleStart: add the challenge MazeBuff and the per-type score targets
  -   (PF: target 10002 on slot 1 + StoryExcel.BattleTargetID on slot 5;
  -   AS: targets 90004/90005 on slot 1). Fixes missing 积分/战意值 UI when
  -   entering PF/AS through the calyx override.

## fix(skill): include CharacterSkillAdv in technique maze-buff backfill

- Contributor: March7thHoney
- Commit: `10c6d5f59fe6828f13f5e2c34195b637af119295`
- Description:
  - Silver Wolf lv.999 (1506) and other TriggerBattle-style technique
  - characters bind their primary maze buff via InBattleBindingType =
  - "CharacterSkillAdv" instead of "CharacterSkill". The previous backfill
  - filter excluded them, so the ability JSON's summon (e.g. Silver Wolf's
  - Pac-Man) had no associated maze buff and its in-battle effect never
  - fired after entering combat.
  - Widen the candidate filter to accept either binding type; primary-buff
  - selection (UseType ∈ SummonUnit/AddBattleBuff/TriggerBattle) and the
  - existing dedup against AvatarSceneInfo.BuffList stay unchanged, so
  - Mar7thLover (Cyrene)/Cerydra/Robin etc. still receive only their canonical entry.

## fix(challenge-story): use FantasticStory target id 10003 for Pure Fiction score header

- Contributor: March7thHoney
- Commit: `e86ccd250001734aa2baeb3c0872c7954eee51c0`
- Description:
  - The client treats battleTargetInfo[1] id as the score-counter mode
  - selector: 10002 = legacy per-wave-settle Pure Fiction, 10003 =
  - FantasticStory continuous-wave container. Sending 10002 caused the
  - client to PVEBattleResult after wave 1 (cap ~2300) instead of after
  - the full 3-wave clear (~40000 for node 1).
  - Switch both the normal challenge entry point and the calyx override
  - mirror so a full clear lands the expected score.

## fix(player): trigger OnLevelChange on new account so AutoUpgradeWorldLevel applies StartTrailblazerLevel

- Contributor: March7thHoney
- Commit: `7bf441e270f41057a9cd35d97e1863df914b65bb`
- Description:
  - (No additional commit description)

## Merge pull request #20 from Mar7thLover/feat/calyx-endgame-mode

- Contributor: Mar7thLover (Cyrene)
- Commit: `0d425dad1da8d589ba6a806899d67030b8a6e9e9`
- Description:
  - feat: 花萼模式完整实现 + 银狼秘技进战修复 + 虚构叙事记分修复

## fix:some small change

- Contributor: Mar7thLover (Cyrene)
- Commit: `881cf19b105383738f2905b5fd4bf3476df0b78d`
- Description:
  - (No additional commit description)

## 导入和导出freesrJson

- Contributor: Cyt
- Commit: `2d19a1f9d2c7f5cfd8b7359ad6138a83342a3a85`
- Description:
  - (No additional commit description)

## 忽略不存在

- Contributor: Cyt
- Commit: `7028c01b2f404f9dd85855ea965adb1b48fc9fe4`
- Description:
  - (No additional commit description)

## Merge branch 'main' of https://github.com/qinfyy/March7thHoney-privFork

- Contributor: Cyt
- Commit: `e0f01fd03026fff50de4fdf8a7f42058715a7ae7`
- Description:
  - (No additional commit description)

## fix(gridfight): emit full settlement sequence after 1-1 battle so client shows results panel

- Contributor: March7thHoney
- Commit: `6cc2060725abdc32f929bbe435c761c531cc1128`
- Description:
  - - Add PreSettle sync notify (unlock + damage stt + lineup hp + section record + player level + gold + reward orbs)
  - - Populate GridFightEndBattleStageNotify with gridFightDamageSttInfo / DHMBKAPKJFN drops / GCBBEEGANEG level info / NNFAFGCGMBB hp timeline; track pre-battle hp snapshot for delta
  - - Rewrite PostBattle sync to include levelSyncInfo for next encounter plus re-lock and pendingAction
  - - Snapshot per-role damage from PVEBattleResultCsReq and hand out 4 orbs (102/199/203/204) + 4 gold + 2 hp for perfect clear; bump player exp/level

## feat(gridfight): randomize starting portal buff trio from season pool

- Contributor: March7thHoney
- Commit: `0c74df3514da87dd5a81364a6844ebe064635586`
- Description:
  - - Add GridFightSeasonPortalExcel loader that builds GameData.GridFightSeasonPortalData (season -> portalId list) from GridFightSeasonPortal.json
  - - Add CurrentPortalBuffOffer + RollPortalBuffs/EnsurePortalBuffOffer/ClearPortalBuffOffer on GridFightInstance, falling back to embedded id list when season data is missing
  - - Replace hardcoded {1115,106,147} in GridFightService/GridFightInstance.ToProto/GridFightManager.BuildCurrentInfo with EnsurePortalBuffOffer/RollPortalBuffs so each player gets a fresh random trio
  - - Make reroll draw 3 new ids excluding the current offer; clear offer on selection
  - - Regenerate Config/Resource.cache so the new dictionary loads on startup

## feat(gridfight): randomize per-session camps and affixes from data tables

- Contributor: March7thHoney
- Commit: `31a8fd7f1adc1d043ec8b7f914473b2425fccd6e`
- Description:
  - - Replace hardcoded {17,8,24} camp trio and {1002,3005,4010} affix triple in BuildLevelSection with random picks stored on the instance
  - - Roll 3 camps from GridFightCampData filtered by SeasonID; roll affixes from GridFightAffixConfigData with count = sum of GridFightDivisionStage.AffixChooseNumList for the chosen division (so A8 财富造物主40 gets 4 instead of 3)
  - - Add EnsureSessionPreview hook on EnsureDefaultRoles so ToProto/GetData picks up the same trio/affixes consistently

## Add TrainCakeCatch social play & train systems

- Contributor: Meliodas
- Commit: `db16e84f7a8611b119bf0dcf23cb8eed366a09ef`
- Description:
  - Basic Implementation of the Train Social System.
  - Some things that are still not working
  - - Co op UI elements
  - - Map tracking in Co op
  - - Joining your own world through the map won't work (You can join your own co op session by joining Hyacine's)
  - - You can't leave your own session by normal means, you can leave anyone else's session just fine.

## feat(gridfight): randomize initial bench roles, starter equipment, and forge goods

- Contributor: March7thHoney
- Commit: `6397d1cdf1cdd04a120760685a5bf69c35558cd9`
- Description:
  - - Drop the static InitialBenchRoles {Asta,Sparkle,Tribbie,YaoGuang at pos 15-18} and roll 4 random roles from GridFightRoleBasicInfoData filtered by SeasonID + IsInPool + 1xxx AvatarID, assigning pos 14-17 to match the official capture
  - - Pull the GP_Avatar_* component string from the role's RoleSavedValueList instead of a hardcoded map
  - - Replace fixed 350602 starter equipment and {350204,350205,350208,350207} forge trio with random picks from GridFightEquipmentData in the 35020x-35089x range
  - - HandlePortalBuffSelect now reads inst.RolledBenchRoles / RollInitialEquipmentId / RollForgeGoods so each run gets a different lineup and inventory drop

## fix(gridfight): stop sending starter equipment/forge that conflicted with pos 14 role

- Contributor: March7thHoney
- Commit: `ca30ffb952aa14df9c6921135e0d51bf061dd81a`
- Description:
  - - HandlePortalBuffSelect was adding a hardcoded equipment (350602/random) and forge item 99999 at pos 14, but my prior change moved the first random bench role to pos 14 too; the client could not process the conflicting state and replayed selectPortalBuff in a loop
  - - Match the official capture (which never sends addGameItemInfo or addForgeInfo at this step) by removing both sections; buy-exp UI is sourced from gridFightGameData.PGFMICHMHFC[uniqueId=8] in GetData and keeps working
  - - Drop the now-unused RollInitialEquipmentId / RollForgeGoods / RolledInitialEquipmentId helpers

## fix(gridfight): stop downgrading max unlocked division to 10706 after settle

- Contributor: March7thHoney
- Commit: `eac3214978e518ac7ea92bcbf8c64b1c85da8c68`
- Description:
  - - GridFightSettleNotify was sending hardcoded EDKIICIKJKL/OHOPKAAKOGF/BBDOCJGAEEJ = 10706, which the client treated as the new max unlocked division and dropped the selector from A9 40 back to A7 6 after the player quit-settled mid-run
  - - BuildCurrentInfo fallback (when no instance exists) and StartGamePlay zero-divisionId default also hardcoded 10706 / 10939
  - - Add MaxDivisionId() helper sourced from GameData.GridFightDivisionInfoData.Keys.Max() and route every hardcoded division id through it so the selector always stays at the top tier
  - - Bump GridFightService.EnsureOrStart default param from 10939 to 10940 for consistency

## fix(gridfight): grant the emblem matching the chosen portal buff and stop forcing 350205 each run

- Contributor: March7thHoney
- Commit: `df43774ca69b3f50ec2bf9ac5d56a6d326169aea`
- Description:
  - - Load ShowBonusIDList (and EffectParamList) from GridFightPortalBuff.json on GridFightPortalBuffExcel; regenerate Config/Resource.cache so the new fields populate
  - - HandlePortalBuffSelect now drives addGameItemInfo from GridFightPortalBuffData[buffId].ShowBonusIDList, filtering through GridFightEquipmentData so non-equipment effects (gold, rule mods, etc) just get skipped instead of crashing; supports multiple emblems per buff
  - - Drop the hardcoded 350205 starter equipment and 350101 consumable on GridFightInstance and the same 350205 fallback in BuildSettleNotify so 1-1 prep no longer ships the Life Flower every match
  - - Earlier EffectParamList[0] choice was wrong: buff 1114 had Effect=35052010 (shield) vs ShowBonus=35052006 (burning blood); the icon on the buff card is the ShowBonus, which is what the player actually receives

## fix(gridfight): make shop buy/refresh sync match official capture

- Contributor: March7thHoney
- Commit: `58ea8804598a7378bba7c05bbfe1bcdadb45d7d9`
- Description:
  - - BuildBuyGoodsSync: emit two GridFightSyncResultData sections under gridUpdateSrc LnpfefkjdhpDpekjiiicgh — first with new gold + addRoleInfo carrying GP_Avatar_* component, second with shopSyncInfo so the bought slot keeps showing but with isSoldOut=true (matches 26-05-17 20.51.37 seq 477)
  - - BuildRefreshShopSync: emit itemValue (new gold) plus shopSyncInfo under gridUpdateSrc LnpfefkjdhpEjkejdnhioe so the refresh actually deducts gold client-side (matches seq 484)
  - - Add BuildShopSyncInfo helper to keep the shop list shape consistent across buy/refresh

## fix(gridfight): roll bench role with matching trait + shop sold-out state + refresh gold cost

- Contributor: March7thHoney
- Commit: `cc19afafb647da77f200f8f0bdad8516edaad3d8`
- Description:
  - - Load PortalGameRefTrait on GridFightPortalBuffExcel; regenerate Config/Resource.cache so the new field populates
  - - RollInitialBenchRoles(count, requiredTrait): when buff has a trait (e.g. 1111 → 2002), seed bench with one role that owns that trait before filling the rest, so the granted emblem actually has a wearer client-side and the UI advances past portal-buff select (matches 26-05-17 20.14.30 seq 459/471 where Constance/YaoGuang showed up alongside the trait-restricted emblem)
  - - MaterializeInitialBenchTeam(requiredTrait): rerolls when previously cached roster lacks the trait
  - - HandlePortalBuffSelect: look up the buff's PortalGameRefTrait[0] and pass it to MaterializeInitialBenchTeam
  - - TryBuyGoods: flip ShopGoods[idx].IsSoldOut so the bought slot disappears from the shop client-side; refuse double-buy
  - - TryRefreshShop: actually deduct the refresh cost (ShopRefreshLeft, which is the price, not a counter) instead of decrementing it
  - - Drop the duplicate NextOrbUniqueId field that was declared twice on GridFightInstance

## Merge pull request #21 from Mar7thLover/origin/feat/traincoop

- Contributor: Mar7thLover (Cyrene)
- Commit: `d4303fc703d4d71dfc0afe03c32922673736c355`
- Description:
  - Add TrainCakeCatch social play & train systems

## Merge pull request #22 from Mar7thLover/feat/gridfight-fixes

- Contributor: Mar7thLover (Cyrene)
- Commit: `4aa4f8982821a4669c1133461ae2b82a1895f670`
- Description:
  - feat(gridfight): 货币战争一系列修复（投资环境/商店/星徽/结算/难度解锁等）

## Merge branch 'Mar7thLover:main' into main

- Contributor: Cyt
- Commit: `dd83fe8ec5be576563c55ee9b9ef3e327b082f61`
- Description:
  - (No additional commit description)

## feat(gridfight): sync DH-like battle/shop/merge flow and add grid debug command

- Contributor: Mar7thLover (Cyrene)
- Commit: `d4dad7667d3666076835ff90bb9b6d5c19c5c65e`
- Description:
  - (No additional commit description)

## Merge pull request #23 from qinfyy/main

- Contributor: Mar7thLover (Cyrene)
- Commit: `16d5be1ed3b3db21822930da2dff705fd7bf4d45`
- Description:
  - Tools

## refactor(calyx/json): merge FreeDataProcess into FreesrShared and fix json export null warning

- Contributor: Mar7thLover (Cyrene)
- Commit: `563cea5a9353175d46918f6e0f89b5a16666024a`
- Description:
  - (No additional commit description)

## fix(freesr): clear old relic/lightcone on sync and persist battle_config

- Contributor: Mar7thLover (Cyrene)
- Commit: `4cbed38600ab8f71e65af8376c0ec2dd307a1553`
- Description:
  - (No additional commit description)

## fix(dispatch): Build the public base URL for dispatch endpoint

- Contributor: Meliodas
- Commit: `e86051cb1f439e2f8a133c565874962dc75d4319`
- Description:
  - (No additional commit description)

## feat(watermark): add configurable watermark options and move to config

- Contributor: Meliodas
- Commit: `ef270448ee2756cd1905ab168f31465d9864ccb1`
- Description:
  - (No additional commit description)

## refactor(calyx): split /calyx toggle from /json import

- Contributor: March7thHoney
- Commit: `979dea74cbf4c3638df10593b58368f7b7aa0813`
- Description:
  - - /calyx on/off now only toggles override flag; no file IO, errors with NoCachedData when no prior /json or srtools sync
  - - /json and srtools persist character data and also cache battle_config + per-avatar battle state (sp_value/enhanced_id/techniques) into CalyxOverrideData.CachedJson
  - - ImportAvatars no longer mutates persistent pathInfo.EnhanceId; freesr enhanced_id only affects calyx-mode battles via CalyxOverrideContext.Apply
  - - ExportPlayerDataAsync reads enhanced_id/sp/techniques from CachedJson when present, falling back to player avatar state
  - - Add Game.Command.Calyx.NoCachedData i18n key (CHS/CHT/EN); drop file path from ToggleOn message
  - - Drop CalyxOverrideManager.EnableLatestOnlyEnemyOverride / LoadFromIndex; add Enable()

## feat(srtools): authenticate by UID instead of username

- Contributor: March7thHoney
- Commit: `7a6136806c6b80b8423f932feedc7fdc4c629468`
- Description:
  - - /sr-tools endpoint now parses the request's `username` field as the player UID and looks it up via AccountData.GetAccountByUid
  - - Returns 400 with a clear message when the field is non-integer or the UID is unknown
  - - Resolves mismatch where the srtools site sends the miHoYo login name (e.g. "1") but the server account is keyed by UID (e.g. 10001)

## Merge pull request #24 from Mar7thLover/feat/calyx-srtools-auth

- Contributor: Mar7thLover (Cyrene)
- Commit: `6a1fb5e1d13e08c553cf35adee2d52158c3113af`
- Description:
  - refactor(calyx/srtools): /calyx 仅控开关 + srtools 按 UID 鉴权

## Add authentication, password & admin APIs

- Contributor: Meliodas
- Commit: `7c97dc04ab430e75d701781909c834fff847228c`
- Description:
  - Introduce server-side authentication and admin tooling: add AuthSecurity for secure password hashing (PBKDF2) and session token generation; extend AccountData with password salt/hash, iterations and token expirations plus helper methods to set/verify passwords and validate tokens. Add AccountHelper password validation, SetPassword and account creation overload that accepts a password. Add admin web UI/API (AdminRoutes, AdminSessionManager) for account management, set password, and deletion (uses DatabaseHelper.DeleteUidData). Add dispatch & server-exchange endpoints to validate account tokens across servers and a ServerExchange secret check. Add registration/login platform pages and routes, update DispatchRoutes to use new handlers and configurable EnvType/registration settings. Add Login RSA key files and a PowerShell keypair generator (Generate-LoginRsaKeyPair.ps1). Add HttpNetwork.SendPostFormRequest helper, logging improvements in token granter, and game server token validation logic when creating player sessions. Update config with Auth options (password policy, token expirations) and ServerExchangeSecret. Misc: various plumbing to persist auth state and safely enumerate account maps.
  - **CLIENT MUST BE RSA PATCHED WITH PUBLIC PEM**

## Add mail attachment parsing and handling

- Contributor: Meliodas
- Commit: `5e8665d3e7d99b88c0812964ae0676ca98439fb4`
- Description:
  - CommandMail: add parsing for _ATTACH/_ATTACHMENT tokens and support item modifiers (x[count], r[rank], l[level]); validate arguments and build a list of ItemData attachments; send mails with attachments and return proper I18N notices on error/success. Added ParseAttachments and TryParseAttachmentModifier helpers.
  - Internationalization: update mail Usage strings (EN/CHS/CHT) to document the new _ATTACH syntax.
  - MailManager: persist sender UID for saving when creating mails; implement robust attachment granting via GrantAttachmentItem which handles multi-grant behavior for AvatarCard/Equipment/Relic, applies rank/level constraints, handles avatar grants and avatar option application, and collects sync items/avatars/new avatar IDs. Send appropriate PlayerSync and PacketAddAvatar notifications for granted items/avatars. Added utility helpers (GrantsOneAtATime, GetAttachmentRank, GetAttachmentLevel, ApplyAvatarAttachmentOptions). Extend TakeMailAttachmentResult with SyncItems, Avatars and NewAvatarIds to return additional sync data.

## feat: Implement command permissions and roles system

- Contributor: Meliodas
- Commit: `ff8a8fa21e12be7ccc14ff0aab58be2a11a476e6`
- Description:
  - - Added CommandPermissions and CommandRoles classes to manage command permissions and roles.
  - - Updated existing command classes to include specific permissions for each command.
  - - Enhanced AccountData to support roles and effective permissions.
  - - Modified AdminRoutes and AdminPageRenderer to handle role management in the admin interface.
  - - Introduced new command for sending welcome mail with attachments.
  - - Updated internationalization files to reflect new command usages and descriptions.
  - - Refactored permission checks across various components to utilize the new permissions system.

## update readme

- Contributor: Mar7thLover (Cyrene)
- Commit: `c6d018a509b26e55b99d5066dbdebf21745acd4e`
- Description:
  - (No additional commit description)

## Temporarily Remove Workflow

- Contributor: Mar7thLover (Cyrene)
- Commit: `4ca4ad18411ba6be994e8c71ad8d2cdae93a3004`
- Description:
  - (No additional commit description)

## feat:add ImplementationTree

- Contributor: Mar7thLover (Cyrene)
- Commit: `f30e36acdff56aa83ad1f79f37067fdbe6074137`
- Description:
  - (No additional commit description)

## fix:readme markdown

- Contributor: Mar7thLover (Cyrene)
- Commit: `e73c33b3324582dc9d75ae2a04d51e15ae248f4a`
- Description:
  - (No additional commit description)

## fix(calyx): relax relic level import and clear equip bindings; rename 9860 relic preset rsp

- Contributor: Mar7thLover (Cyrene)
- Commit: `cee6e57d9a319bb023a128c788154c6dee08c0c5`
- Description:
  - (No additional commit description)

## fix(calyx): restore original lightcone cleanup path and keep first-import equip fix

- Contributor: Mar7thLover (Cyrene)
- Commit: `4b2c3802eb98ce2c56b74f2e44e0b36418ac5d82`
- Description:
  - (No additional commit description)

## fix typo

- Contributor: Mar7thLover (Cyrene)
- Commit: `e0658fb0399247c1413a1ee29cc73f9d11c9810f`
- Description:
  - (No additional commit description)

## fix2

- Contributor: Mar7thLover (Cyrene)
- Commit: `90674efc381b62633dc983ac0a7dfeb62600cd04`
- Description:
  - (No additional commit description)

## feat(mail): enhance mail system with bulk sending and attachment handling

- Contributor: Meliodas
- Commit: `c1c141d5b61443479e4d729d8121c25b583f19cf`
- Description:
  - (No additional commit description)

## feat: update command permissions and roles for various commands

- Contributor: Meliodas
- Commit: `69cf3e2ed2749bf4df7624759c1fc1f2b1d2bbd4`
- Description:
  - (No additional commit description)

## feat(account): add reset command and related database handling

- Contributor: Meliodas
- Commit: `8a0a5ad4baec4b0f75768a5a489bb6edd14de7c5`
- Description:
  - (No additional commit description)

## feat(gridfight): add 1-3 augment, 1-5 supply, 1-7 elite branch flows

- Contributor: March7thHoney
- Commit: `0c9ac419be2465fee9816b8dacc8695f769f70a5`
- Description:
  - - Default RouteId switched to 1200 so chapter 1 follows the canonical 9-node layout (1-3 augment, 1-5 supply, 1-7 elite branch).
  - - ResolveRoute keyed by inst.RouteId for deterministic lookups; ResolveWave skips monster rolls for Supply; ResolveCamp rotates by section instead of always returning SessionCampIds[0].
  - - GridFightInstance now tracks CurrentAugmentOffer / CurrentSupplyOffer / CurrentEliteBranchOptions plus Roll/Clear helpers; BuildSectionEntryPending dispatches eliteBranchAction / supplyAction / augmentAction based on the next node's NodeType + IsAugment.
  - - PendingActionProcessor handles AugmentAction + RerollAugment, SupplyAction (1-indexed selectSupplyIndexes, auto-advances SectionId on consume) + RerollSupply, EliteBranchAction + HGKMHOOFNDA reroll; HandleReturnPreparation skips re-Resolve when EliteBranch was just consumed so the chosen encounter survives.
  - - BuildPostBattleSync emits the 2-encounter preview for EliteBranch sections (PenaltyRule LFKBMDHKPFI derived from groupBase+quality matching GridFightBinaryNodeRule + GridFightPenaltyRule), and omits routeEncounterList for Supply to avoid section/state mismatch.
  - - New HandlerHNEJHAJJIOB handles cmdId 8469 (alt encounter-select path some clients use); HandlerGridFightEnterBattleStageCsReq short-circuits non-combat (Supply) nodes.
  - - GridFightStageRouteExcel exposes ParamList so encounter IDs / penalty rule prefixes can be derived per route; AugmentPoolJL/KD on GridFightManager promoted to static arrays reused by RollAugments and the handbook builder.

## feat: Implement email verification and password recovery features

- Contributor: Meliodas
- Commit: `e82cfb132dc0e8dd48c1e1ed8b443b84373c2418`
- Description:
  - - Added email verification functionality to account login and registration processes.
  - - Introduced AccountEmailSender for sending verification and password reset emails using MailKit.
  - - Created AccountRecoveryHandler to manage email verification and password reset requests.
  - - Developed new pages for password forgot and password reset with corresponding UI and logic.
  - - Updated existing login handlers to include email verification status in responses.
  - - Enhanced admin page to manage user email verification status and send verification emails.
  - - Refactored LoginResJson and PassportLoginResJson to include email verification status.
  - - Added new request bodies for sending verification emails and handling password resets.
  - - Updated project dependencies to include MailKit for email handling.

## feat: Enhance authentication and email handling with token expiration updates and HTML email templates

- Contributor: Meliodas
- Commit: `d25ecd87dd3ce2e825e252eeadeb940d919f4c01`
- Description:
  - (No additional commit description)

## fix(permissions): add new command permissions and roles for Avatar and Grid

- Contributor: Meliodas
- Commit: `72e393777432ceace866eafd4da7593cf4ab38f4`
- Description:
  - (No additional commit description)

## fix(gridfight): make /grid section command actually teleport in chapter 1

- Contributor: March7thHoney
- Commit: `60a3a4aae85c22cdab607cbf604109e83f3952f2`
- Description:
  - - Previously /grid section only mutated server-side SectionId without notifying the client, so the UI stayed on the original section and 出战 still launched the old battle.
  - - Now emits the PostBattle sync + RoundBegin pending pair (mirroring the natural post-battle path), so the client transitions to the target section's prep view and HandleRoundBegin -> BuildSectionEntryPending dispatches the right pending: supplyAction for 1-5, augmentAction for 1-3 (CampMonster + IsAugment), eliteBranchAction for 1-7, returnPreparationAction for plain combat nodes.
  - - Resets LastAugment/Supply/EliteBranchConsumedSection + Current*Offer + reroll counters and clears BattleComponent so the target node's pending fires cleanly.
  - - Restricts to chapter 1 sections 1..9 since cross-chapter transitions and 1-10 fallthrough are not implemented yet; out-of-range args return InvalidArguments.
  - - Pre-configures BattleComponent for combat nodes via ConfigureNextBattle so EnterBattleStage has the right monsters; Supply nodes stay empty (relies on existing IsCombatNode short-circuit in HandlerGridFightEnterBattleStageCsReq).

## feat(gridfight): support chapter transition (1-9 -> 2-1) and use real chapter id

- Contributor: March7thHoney
- Commit: `755402697ba85684a7ff4209d185e727609971e5`
- Description:
  - - OnBattleResolved now rolls over to next chapter (CurrentChapterId++, SectionId=1) instead of producing an out-of-range SectionId when the player wins the final section of the current chapter. Chapter size is derived dynamically from the route bucket via the new GetChapterSectionCount helper (chapter 1=9, chapter 2/3=7 for route 1200), so 1-10 / 2-8 ghost sections are gone.
  - - On chapter change we also reset LastAugment/Supply/EliteBranchConsumedSection + Clear*Offer + reroll counters so the new chapter's augment / supply / elite branch nodes fire cleanly.
  - - BuildPostBattleSync's probe now bumps probeChapter / resets probeSection when it would overflow, and emits levelInfo.DCPKPNLKGMM = probeChapter so the client sees the right chapter (matches official seq 500 DCPKPNLKGMM=2 sectionId=1 after 1-9 boss).
  - - Replaces inst.Level (hardcoded const 1) with inst.CurrentChapterId for every DCPKPNLKGMM assignment across GridFightInstance, GridFightManager, PendingActionProcessor and PacketGridFightEnterBattleStageScRsp. Previously chapter 2 battles would still report DCPKPNLKGMM=1 and the client mis-rendered chapter progress.
  - - /grid section relaxes its bounds: chapter <= 3 plus per-chapter max-section check via GetChapterSectionCount, so /grid section 2 5 jumps straight to 2-5 elite branch.

## Refactor inventory management and player login handling

- Contributor: Meliodas
- Commit: `012e22322d47c9a210dbc0d6ef5b597a32a18c47`
- Description:
  - - Updated InventoryManager to improve item synchronization and handling of equipment level-up and rank-up processes.
  - - Introduced MonthCardService to manage month card rewards and expiration logic.
  - - Modified packet handling for equipment level-up and rank-up requests to include return codes.
  - - Enhanced player login handling to check for month card rewards and send appropriate notifications.
  - - Added new packet classes for rank-up equipment responses and improved existing packet structures for consistency.

## feat: Implement account ban functionality and identity key management

- Contributor: Meliodas
- Commit: `36b33158c7b828458b208fa24ab89cc145f2c74c`
- Description:
  - - Added properties for account banning in AccountData, including IsBanned, BanReason, and timestamps for ban management.
  - - Introduced AccountBanStatus and AccountBanHelper to handle ban logic and identity key management.
  - - Updated CommandUnlockAll to kick players with a specific reason when unlocking all.
  - - Enhanced login handlers to check for active bans using identity keys and respond accordingly.
  - - Implemented admin routes for banning and unbanning accounts, including validation for ban duration and reason.
  - - Modified the admin page to display ban status and allow for banning/unbanning users.
  - - Added utility functions for managing device identity keys and checking for active bans based on those keys.

## Refactor account management and database saving logic

- Contributor: Meliodas
- Commit: `38c5c746640d99d3ba4d0832237c6987f573c127`
- Description:
  - - Updated methods in CommandAccount, CommandJson, CommandKick, CommandUnlockAll, and other command classes to use the new MarkUidForSave method instead of ToSaveUidList.
  - - Introduced ConnectionDisconnectHelper to handle player disconnections more cleanly.
  - - Modified AccountData methods to support optional persistence when banning and clearing bans.
  - - Enhanced DatabaseHelper to queue database saves more efficiently.
  - - Adjusted various handlers and managers to ensure proper handling of player states and database updates.

## feat: Add remove command for avatars and equipment

- Contributor: Meliodas
- Commit: `8f9e971955faa6b25f18cb67dd8c67ae588abe6f`
- Description:
  - - Implemented CommandRemove class to handle removal of avatars and equipment.
  - - Added methods to remove avatars and light cones, including confirmation and dry-run options.
  - - Updated internationalization strings for new command usage.
  - - Enhanced HandbookGenerator to include command usage notes and improved command descriptions.
  - - Added new command descriptions for Clear, Debug, Remove, and Windy in LanguageEN.cs.

## feat(gridfight): final-boss (3-7) settlement flow + reset on season finish

- Contributor: March7thHoney
- Commit: `708c9268f71c492c3dda2b291146652386ec16de`
- Description:
  - - EndBattle: detect final section of final chapter -> emit SyncKeepWinCntNotify + SettleNotify and null GridFightInstance so the next StartGamePlay reboots from 1-1 (instead of advancing into nonexistent chapter 4)
  - - OnBattleResolved: guard chapter rollover when next chapter has no route data; move KeepWinCnt increment into EndBattle so EndBattleStageNotify reports the post-increment count
  - - BuildEndBattleNotify: add PACIAIJBOHO, JGLEDADBNGP, IDEAAPCCFPF, IPCFJBAKLCG, MAGCGPMHHEA and bump PNOJLNNHBIH so the client renders the 挑战成功 boss-end screen instead of 挑战失败
  - - BuildSettleNotify: set Reason from inst.LastSettleReason (overridable), populate HBNHKPDMGIP, EABPCKEDDBH, PHDEOPEJIID, GridFightTraitInfo (mapping IENNMHMOONM -> GridGameTraitInfo), GridGameRoleList, and use CurrentChapterId instead of the hardcoded inst.Level

## Add AdminPageStyles class with comprehensive CSS styles for admin interface

- Contributor: Meliodas
- Commit: `cbcd16793c63f0153d05b3a1e65557b7c106464f`
- Description:
  - (No additional commit description)

## Merge pull request #25 from Mar7thLover/feat/gridfight-full-loop

- Contributor: Mar7thLover (Cyrene)
- Commit: `7113e5cc4f61dd4dd85ee9a1a9da86b1435f060d`
- Description:
  - feat(gridfight): full season loop, chapter transition, /grid section command

## Merge pull request #26 from Mar7thLover/origin/feat/auth

- Contributor: Mar7thLover (Cyrene)
- Commit: `55fbaff76231413eff41d6e9ed647124865a6416`
- Description:
  - Origin/feat/auth

## fix(auth): align rsa decrypt and auto-create login bypass behavior

- Contributor: Mar7thLover (Cyrene)
- Commit: `f9213a71d021d7abe787829224d3573929e9c932`
- Description:
  - (No additional commit description)

## fix(gridfight): remove Sparxie forced avatar saved-value injection

- Contributor: Mar7thLover (Cyrene)
- Commit: `6283be624ee349111464d12f014a01eec8ffdb54`
- Description:
  - (No additional commit description)

## fix(web): avoid admin redirect 500 when response already started

- Contributor: Mar7thLover (Cyrene)
- Commit: `873913d48f5da2bcad7bdba81641d4ec96823ae7`
- Description:
  - (No additional commit description)

## chore(config): update default auth and server option values

- Contributor: Mar7thLover (Cyrene)
- Commit: `b6c26047b7274f5e5a3009ff24a7fb809ecce0c3`
- Description:
  - (No additional commit description)

## fix:account cmd and handbook use i18n

- Contributor: Mar7thLover (Cyrene)
- Commit: `296d12c461488d31eb8f0583f89a462c0cc70ec5`
- Description:
  - (No additional commit description)

## small change

- Contributor: Mar7thLover (Cyrene)
- Commit: `960803640fe3451a02abcfe96802cd42470fe3b0`
- Description:
  - (No additional commit description)

## feat(gridfight): equip dress + carry equipment into battle

- Contributor: March7thHoney
- Commit: `b33b41913642fb4e0fb8af1d51fdb7829958fee0`
- Description:
  - - GridFightInstance: track role -> equipped equipment uniqueIds via EquipUniqueIdsByRoleUniqueId
  - - HandlerGridFightEquipDressCsReq: actually attach the equipment to the role and send updateRoleInfo with the full UpdateEquipsComponent list (supports multiple equips per role)
  - - PacketGridFightEnterBattleStageScRsp: populate JAJOBJJPINN.HHIJFHKJEPL from the dressed equipment so equipment icons show under the avatar in battle

## fix(gridfight): register portal-buff equipment into instance inventory

- Contributor: March7thHoney
- Commit: `e2b6a93ed329a5eeb1ace43964882e2db5bdebbc`
- Description:
  - HandlePortalBuff only added the granted emblem to the AddGameItemInfo
  - sync packet but skipped inst.Equipments. The equip-dress handler checks
  - inst.Equipments membership before attaching, so emblems obtained from the
  - opening investment (e.g. Wolf Hunt invitation) were silently rejected
  - while command-granted emblems still worked.

## fix(gridfight): include emblem-granted traits in battle trait count

- Contributor: March7thHoney
- Commit: `384ddbb3942eff5fff4fd826384d36125b848daf`
- Description:
  - CheckTrait only summed each role's intrinsic TraitList, so emblems
  - worn by a role were ignored once combat started. The prep screen
  - counted them client-side, hence the mismatch between "3 Bliss / 2
  - Hunter / 2 Star Voyager" in prep and "1 / 2 / 1" in battle.
  - Now each role's trait set unions its intrinsic traits with the
  - OriginEmblem/ClassEmblem trait id from every equipped emblem before
  - counting, so the in-battle layer matches what the prep UI shows.

## Merge pull request #27 from Mar7thLover/feat/gridfight-emblem-fixes

- Contributor: Mar7thLover (Cyrene)
- Commit: `c62732ebf684b8cb94db508ff0af84cd661415ed`
- Description:
  - fix(gridfight): equip dress now works and shows in battle

## Fix GridFight battle difficulty payload, RSA auto-login decrypt flow, and handbook naming

- Contributor: Mar7thLover (Cyrene)
- Commit: `7aa44a17d2dec40c239271a2ce1cda0d7b444689`
- Description:
  - (No additional commit description)

## Add configurable login announcement popup and /anno broadcast command

- Contributor: Mar7thLover (Cyrene)
- Commit: `fa8c29c18af5ad5e318796f2339df1e73ae90624`
- Description:
  - (No additional commit description)

## fix: stabilize mission tracking and gender lineup handling

- Contributor: Mar7thLover (Cyrene)
- Commit: `1483a0066050f367063c0d94170824f346b69a92`
- Description:
  - (No additional commit description)

## fix: update CommandPermissions and CommandRoles to be consistent with the rest

- Contributor: Meliodas
- Commit: `906d4e32af3cffa075b6d9e715733a1aa14e73fe`
- Description:
  - (No additional commit description)

## fix(gridfight): emit trait bonus level reward map for activated trait effects

- Contributor: March7thHoney
- Commit: `58212ecdc45c58f3913659b4118a462430fc4f71`
- Description:
  - CheckTrait now populates BattleGridFightTraitEffectInfo.TraitEffectLevelInfo
  - when the trait effect is of TraitBonus type, looking up the threshold->reward
  - map via GridFightTraitBonus -> GridFightCombinationBonus ->
  - GridFightBasicBonusPoolV2. Without this, the client black-screened and KCP
  - timed out when 4 owned formal characters (e.g. 蝶/风/月/昔) were placed in
  - the front row together, because at count >= 2/3 trait 1012 layer 2 activated
  - effect 10121 (TraitBonus type) and the client expected a reward map that was
  - never sent.

## fix(gridfight): align plane bosses with preview and rebalance battle waves

- Contributor: March7thHoney
- Commit: `c9d9efce330236a494db1c9fb6d7b3ec5aa93b60`
- Description:
  - - restrict session camps to those with BossBattleArea and tier>=5 monsters
  - - precompute and emit bossInfo.IJOPBPABPPM so 1-9/2-7/3-7 match the preview
  - - pin each plane to SessionCampIds[chapter-1] instead of round-robin
  - - bump CampMonster/EliteBranch waves to ~10 monsters; first two combat
  -   nodes of each plane stay tier-2 only with 0-1 elites, later nodes mix
  -   in tier-3 and 2-3 elites
  - Co-Authored-By: Claude Opus 4.7 <noreply@anthropic.com>

## feat: implement configurable server announcements with duration and frequency options

- Contributor: Meliodas
- Commit: `285e165c339d48106589385c0dafe34b0c2a2ee3`
- Description:
  - (No additional commit description)

## refactor: update connection handling and gameplay data saving methods for async support

- Contributor: Meliodas
- Commit: `980befe29d5625989fb66e2008e58769c9390da9`
- Description:
  - (No additional commit description)

## fix(gridfight): force tier-3 monsters into elite slots for late battles

- Contributor: March7thHoney
- Commit: `365ac69a88fa22a8b25a597c581716e478f3d18a`
- Description:
  - - elite is determined by monster tier 3+ model, not roleStar; pull elites
  -   from the camp's tier-3 pool explicitly
  - - late CampMonster nodes (1-6/2-4/3-4): 1-2 tier-3 monsters per wave
  - - EliteBranch encounters (1-7/2-5/3-5): 2-3 tier-3 monsters per wave
  - - early nodes (first two combat nodes per plane): keep tier-2 only,
  -   0-1 star=2 enhancement
  - - tier-3 picks get 50% star=2 to match the official distribution

## fix(gridfight): only drop orbs on reward nodes and emit per-orb sync

- Contributor: March7thHoney
- Commit: `bb55f9fa386a910731df721a43742c18262b3dca`
- Description:
  - - skip orb drops in RecordBattleSnapshot when current section's NodeType
  -   is not Monster (reward); CampMonster/EliteBranch/Boss now keep gold+exp
  -   baseline but produce no orbs
  - - randomize 4-5 orbs per reward node from a {102/103, 199, 203, 204} pool
  - - new TryUseOrbsDetailed returns per-orb result so the handler can build
  -   one SyncResultData per orb with gridUpdateSrc=OOPNBCIJHMP and
  -   syncEffectParamList=[uniqueId, itemId], matching official; fixes the
  -   'click does not hide orb' bug
  - - orb 199=+2 gold, 102/103=+1 gold + 350101/350103 consumable,
  -   203=random 350201-208 equipment, 204=+3 gold; 101XXX/211XXX/311XXX
  -   granting role into the bench
  - - post-settle syncEffectParamList second slot now carries real sectionId
  -   (was hardcoded 1)

## Merge pull request #28 from Mar7thLover/fix/gridfight-battle-boss-orb

- Contributor: Mar7thLover (Cyrene)
- Commit: `bc22c626fcb28a599aea2a47d502748231f3070f`
- Description:
  - fix(gridfight): trait effect, plane boss, battle waves, orbs

## fix(gridfight): implement equip craft, wrench unequip, and furnace roll

- Contributor: March7thHoney
- Commit: `c8ed55f6e83f41e79e7bef215e4576544449ec85`
- Description:
  - - EquipCraftCsReq: actually consume CraftMaterials and add the target
  -   advanced equipment with source=5; sync uses LnpfefkjdhpDgojihijlaf src
  - - UseConsumableCsReq: route by GridFightConsumables.ConsumableRule
  -   - Remove (350101 wrench) -> unequip every item on target role
  -   - Roll (350103 furnace) -> reroll one equipment, or all equipments on
  -     the targeted role, into another item of the same EquipCategory
  - - decrement consumable stack via updateGridFightConsumableList + -1, and
  -   emit one SyncResultData with LnpfefkjdhpNefkflkampo src and
  -   syncEffectParamList=[itemId], matching the official packet shape
  - - new GridFightInstance helpers: FindEquipment, RemoveEquipmentByUniqueId,
  -   AddEquipment, RollEquipment, UnequipAllFromRole, TryConsumeConsumable,
  -   RollSameCategoryEquipment

## fix(gridfight): auto-merge two basic equipments dressed on the same role

- Contributor: March7thHoney
- Commit: `00076beac1d18707c0976123010e382782f20794`
- Description:
  - When EquipDress lands a second Basic-category equipment on a role that
  - already has one, server now crafts them into the corresponding advanced
  - equipment (formula: 35020A + 35020B -> 35030_min(A,B)_max(A,B)) before
  - returning the role update.
  - Sync order matches manual craft: first emit a LnpfefkjdhpDgojihijlaf
  - section with RemoveGameItemInfo per consumed basic + AddGameItemInfo for
  - the advanced, then the regular UpdateRoleInfo carrying the advanced
  - equip uid in updateEquipsComponent.

## fix(gridfight): stack consumables correctly and pick right sync wrapper

- Contributor: March7thHoney
- Commit: `24cd3156b349293d14360adff20da2cf420dfe8d`
- Description:
  - Official packet capture proved the sync wrapper field is what the client
  - uses to decide stack semantics:
  - - removeGameItemInfo deletes the inventory entry entirely (wrong for
  -   decrementing a stack of >1)
  - - updateGameItemInfo with itemStackCount=-1, num=newTotal decrements
  -   the stack
  - - addGameItemInfo with itemStackCount=1, num=1 is only used on the
  -   first instance; subsequent stack-ups use updateGameItemInfo
  - Apply to both command-given and orb-given consumables (they must behave
  - identically):
  - - HandlerGridFightUseConsumableCsReq: emit updateGameItemInfo when
  -   remaining>0, removeGameItemInfo only when going to 0; skip the
  -   removal entirely for IfConsume=false items (350102 gold wrench)
  - - CommandGrid.AddConsumable: use addGameItemInfo on first add,
  -   updateGameItemInfo on subsequent adds, num=new total
  - - Orb path ApplyConsumable: stack server-side per itemId, surface the
  -   new total via OrbUseResult.AddConsumableNewTotal, and the handler
  -   picks Add/Update wrapper based on it

## fix(gridfight): surface chosen environment and strategy in the top bar

- Contributor: March7thHoney
- Commit: `8f33644bda84d91088919a562a104a3dc1010092`
- Description:
  - - ActivePortalBuffIds no longer seeds 106 (二手市场); the bar now shows
  -   only the environment the player actually picked at game start
  - - track selected augments in new ActiveAugmentIds and persist them in
  -   the bootstrap snapshot via GridFightGameAugmentSync.SyncAugmentInfo
  -   so a reconnect still shows them
  - - HandleAugmentSelect now emits a LnpfefkjdhpKcefflkabgh section with
  -   AugmentSyncInfo.UpdateAugmentInfo { AugmentId, MHMLMKDFJLN=true },
  -   matching the official packet (seq 562) so the chosen 投资策略 appears
  -   in the top bar immediately

## fix(gridfight): align enemy difficulty payload with the official packet

- Contributor: March7thHoney
- Commit: `71622c77237d87a9080379543cda323c6a7b7bcd`
- Description:
  - Two new official captures (D8 EDL=108 with gold investments, and D1
  - EDL=61 with prismatic) showed the actual difficulty formula and per-
  - stage EliteGroup progression used by the client.
  - - new GridFightBinaryDiffAddRuleExcel + BinaryNodeDiffAddRule field on
  -   GridFightDivisionStage so we can look up the encounter-quality
  -   EnemyDifficultyAddValue from data
  - - GridFightInstance:
  -   - GetDivisionLevel / GetActiveAugmentDifficultyAdd /
  -     GetEncounterDifficultyAdd / GetEffectiveEnemyDifficultyLevel
  -     compute OOPPKDAFFDG = base + sum(augment delta from
  -     GridFightAugmentMonster[divLv][quality]) + encounter delta from
  -     GridFightBinaryDiffAddRule[stage rule][encounter quality]
  -   - ResolveEliteGroupForCurrentSection walks the route table and
  -     assigns EG = 1800 + global combat index for combat nodes
  -     (1-3=1801, 1-4=1802, 1-6=1803, 1-7=1804, 1-9=1805, 2-1=1806...),
  -     and 1816/1817/1818/1819 for reward nodes by position
  -   - track LastEncounterQuality + chapter/section it applies to
  - - HandleEliteBranchSelect persists the chosen DifficultyTier so the
  -   encounter battle picks it up via GetEncounterDifficultyAdd
  - - PacketGridFightEnterBattleStageScRsp:
  -   - OOPPKDAFFDG = GetEffectiveEnemyDifficultyLevel (was constant base)
  -   - ANBB/HDCA = count of avatars actually placed in pos 1-13 (was
  -     hardcoded 3, then briefly avatar cap which over-counted partial
  -     lineups)
  -   - SyncAugmentInfo populated with ActiveAugmentIds so the client
  -     applies investment-strategy modifiers to monsters
  - - GridFightBattleProtoBuilder BDCCEFHMFHO = WorldLevel - 1 (matches
  -   captures across both divisions)
  - - GridFightLevelResolver: reward/combat/boss waves all use
  -   ResolveEliteGroupForCurrentSection for their EliteGroup

## Merge pull request #29 from Mar7thLover/fix/gridfight-equip-display-difficulty

- Contributor: Mar7thLover (Cyrene)
- Commit: `9b0c4bf698fc18ed507a4aa2522d546495665dd0`
- Description:
  - fix(gridfight): equipment system, env/strategy display, enemy difficulty

