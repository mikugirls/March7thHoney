



#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace March7thHoney.Proto {

  
  public static partial class GCACHJFLGHHReflection {

    #region Descriptor
    
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GCACHJFLGHHReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChFHQ0FDSEpGTEdISC5wcm90bxoRRUZFR0tESEVNRk4ucHJvdG8aGVBsYW5l",
            "dEZlc1F1ZXN0U3RhdGUucHJvdG8itwEKC0dDQUNISkZMR0hIEiQKBnN0YXR1",
            "cxgCIAEoDjIULlBsYW5ldEZlc1F1ZXN0U3RhdGUSEAoIcHJvZ3Jlc3MYBSAB",
            "KA0SEAoIcXVlc3RfaWQYCiABKA0SEwoLT0pCRUFDR0xMT0gYDiABKA0SIwoL",
            "Q0hGTkxKUEVFTkIYASABKAsyDC5FRkVHS0RIRU1GTkgAEhUKC0tHQUFCQ0dH",
            "SE5NGAsgASgNSABCDQoLQUdGQ1BHSFBFRERCFqoCE01hcmNoN3RoSG9uZXku",
            "UHJvdG9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::March7thHoney.Proto.EFEGKDHEMFNReflection.Descriptor, global::March7thHoney.Proto.PlanetFesQuestStateReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::March7thHoney.Proto.GCACHJFLGHH), global::March7thHoney.Proto.GCACHJFLGHH.Parser, new[]{ "Status", "Progress", "QuestId", "OJBEACGLLOH", "CHFNLJPEENB", "KGAABCGGHNM" }, new[]{ "AGFCPGHPEDD" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class GCACHJFLGHH : pb::IMessage<GCACHJFLGHH>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<GCACHJFLGHH> _parser = new pb::MessageParser<GCACHJFLGHH>(() => new GCACHJFLGHH());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<GCACHJFLGHH> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::March7thHoney.Proto.GCACHJFLGHHReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GCACHJFLGHH() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GCACHJFLGHH(GCACHJFLGHH other) : this() {
      status_ = other.status_;
      progress_ = other.progress_;
      questId_ = other.questId_;
      oJBEACGLLOH_ = other.oJBEACGLLOH_;
      switch (other.AGFCPGHPEDDCase) {
        case AGFCPGHPEDDOneofCase.CHFNLJPEENB:
          CHFNLJPEENB = other.CHFNLJPEENB.Clone();
          break;
        case AGFCPGHPEDDOneofCase.KGAABCGGHNM:
          KGAABCGGHNM = other.KGAABCGGHNM;
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GCACHJFLGHH Clone() {
      return new GCACHJFLGHH(this);
    }

    
    public const int StatusFieldNumber = 2;
    private global::March7thHoney.Proto.PlanetFesQuestState status_ = global::March7thHoney.Proto.PlanetFesQuestState.GkbampagnbeAldgogpgnmo;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::March7thHoney.Proto.PlanetFesQuestState Status {
      get { return status_; }
      set {
        status_ = value;
      }
    }

    
    public const int ProgressFieldNumber = 5;
    private uint progress_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint Progress {
      get { return progress_; }
      set {
        progress_ = value;
      }
    }

    
    public const int QuestIdFieldNumber = 10;
    private uint questId_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint QuestId {
      get { return questId_; }
      set {
        questId_ = value;
      }
    }

    
    public const int OJBEACGLLOHFieldNumber = 14;
    private uint oJBEACGLLOH_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint OJBEACGLLOH {
      get { return oJBEACGLLOH_; }
      set {
        oJBEACGLLOH_ = value;
      }
    }

    
    public const int CHFNLJPEENBFieldNumber = 1;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::March7thHoney.Proto.EFEGKDHEMFN CHFNLJPEENB {
      get { return aGFCPGHPEDDCase_ == AGFCPGHPEDDOneofCase.CHFNLJPEENB ? (global::March7thHoney.Proto.EFEGKDHEMFN) aGFCPGHPEDD_ : null; }
      set {
        aGFCPGHPEDD_ = value;
        aGFCPGHPEDDCase_ = value == null ? AGFCPGHPEDDOneofCase.None : AGFCPGHPEDDOneofCase.CHFNLJPEENB;
      }
    }

    
    public const int KGAABCGGHNMFieldNumber = 11;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint KGAABCGGHNM {
      get { return HasKGAABCGGHNM ? (uint) aGFCPGHPEDD_ : 0; }
      set {
        aGFCPGHPEDD_ = value;
        aGFCPGHPEDDCase_ = AGFCPGHPEDDOneofCase.KGAABCGGHNM;
      }
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasKGAABCGGHNM {
      get { return aGFCPGHPEDDCase_ == AGFCPGHPEDDOneofCase.KGAABCGGHNM; }
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearKGAABCGGHNM() {
      if (HasKGAABCGGHNM) {
        ClearAGFCPGHPEDD();
      }
    }

    private object aGFCPGHPEDD_;
    
    public enum AGFCPGHPEDDOneofCase {
      None = 0,
      CHFNLJPEENB = 1,
      KGAABCGGHNM = 11,
    }
    private AGFCPGHPEDDOneofCase aGFCPGHPEDDCase_ = AGFCPGHPEDDOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AGFCPGHPEDDOneofCase AGFCPGHPEDDCase {
      get { return aGFCPGHPEDDCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearAGFCPGHPEDD() {
      aGFCPGHPEDDCase_ = AGFCPGHPEDDOneofCase.None;
      aGFCPGHPEDD_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as GCACHJFLGHH);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(GCACHJFLGHH other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Status != other.Status) return false;
      if (Progress != other.Progress) return false;
      if (QuestId != other.QuestId) return false;
      if (OJBEACGLLOH != other.OJBEACGLLOH) return false;
      if (!object.Equals(CHFNLJPEENB, other.CHFNLJPEENB)) return false;
      if (KGAABCGGHNM != other.KGAABCGGHNM) return false;
      if (AGFCPGHPEDDCase != other.AGFCPGHPEDDCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Status != global::March7thHoney.Proto.PlanetFesQuestState.GkbampagnbeAldgogpgnmo) hash ^= Status.GetHashCode();
      if (Progress != 0) hash ^= Progress.GetHashCode();
      if (QuestId != 0) hash ^= QuestId.GetHashCode();
      if (OJBEACGLLOH != 0) hash ^= OJBEACGLLOH.GetHashCode();
      if (aGFCPGHPEDDCase_ == AGFCPGHPEDDOneofCase.CHFNLJPEENB) hash ^= CHFNLJPEENB.GetHashCode();
      if (HasKGAABCGGHNM) hash ^= KGAABCGGHNM.GetHashCode();
      hash ^= (int) aGFCPGHPEDDCase_;
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (aGFCPGHPEDDCase_ == AGFCPGHPEDDOneofCase.CHFNLJPEENB) {
        output.WriteRawTag(10);
        output.WriteMessage(CHFNLJPEENB);
      }
      if (Status != global::March7thHoney.Proto.PlanetFesQuestState.GkbampagnbeAldgogpgnmo) {
        output.WriteRawTag(16);
        output.WriteEnum((int) Status);
      }
      if (Progress != 0) {
        output.WriteRawTag(40);
        output.WriteUInt32(Progress);
      }
      if (QuestId != 0) {
        output.WriteRawTag(80);
        output.WriteUInt32(QuestId);
      }
      if (HasKGAABCGGHNM) {
        output.WriteRawTag(88);
        output.WriteUInt32(KGAABCGGHNM);
      }
      if (OJBEACGLLOH != 0) {
        output.WriteRawTag(112);
        output.WriteUInt32(OJBEACGLLOH);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (aGFCPGHPEDDCase_ == AGFCPGHPEDDOneofCase.CHFNLJPEENB) {
        output.WriteRawTag(10);
        output.WriteMessage(CHFNLJPEENB);
      }
      if (Status != global::March7thHoney.Proto.PlanetFesQuestState.GkbampagnbeAldgogpgnmo) {
        output.WriteRawTag(16);
        output.WriteEnum((int) Status);
      }
      if (Progress != 0) {
        output.WriteRawTag(40);
        output.WriteUInt32(Progress);
      }
      if (QuestId != 0) {
        output.WriteRawTag(80);
        output.WriteUInt32(QuestId);
      }
      if (HasKGAABCGGHNM) {
        output.WriteRawTag(88);
        output.WriteUInt32(KGAABCGGHNM);
      }
      if (OJBEACGLLOH != 0) {
        output.WriteRawTag(112);
        output.WriteUInt32(OJBEACGLLOH);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Status != global::March7thHoney.Proto.PlanetFesQuestState.GkbampagnbeAldgogpgnmo) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Status);
      }
      if (Progress != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Progress);
      }
      if (QuestId != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(QuestId);
      }
      if (OJBEACGLLOH != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(OJBEACGLLOH);
      }
      if (aGFCPGHPEDDCase_ == AGFCPGHPEDDOneofCase.CHFNLJPEENB) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(CHFNLJPEENB);
      }
      if (HasKGAABCGGHNM) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(KGAABCGGHNM);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(GCACHJFLGHH other) {
      if (other == null) {
        return;
      }
      if (other.Status != global::March7thHoney.Proto.PlanetFesQuestState.GkbampagnbeAldgogpgnmo) {
        Status = other.Status;
      }
      if (other.Progress != 0) {
        Progress = other.Progress;
      }
      if (other.QuestId != 0) {
        QuestId = other.QuestId;
      }
      if (other.OJBEACGLLOH != 0) {
        OJBEACGLLOH = other.OJBEACGLLOH;
      }
      switch (other.AGFCPGHPEDDCase) {
        case AGFCPGHPEDDOneofCase.CHFNLJPEENB:
          if (CHFNLJPEENB == null) {
            CHFNLJPEENB = new global::March7thHoney.Proto.EFEGKDHEMFN();
          }
          CHFNLJPEENB.MergeFrom(other.CHFNLJPEENB);
          break;
        case AGFCPGHPEDDOneofCase.KGAABCGGHNM:
          KGAABCGGHNM = other.KGAABCGGHNM;
          break;
      }

      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            global::March7thHoney.Proto.EFEGKDHEMFN subBuilder = new global::March7thHoney.Proto.EFEGKDHEMFN();
            if (aGFCPGHPEDDCase_ == AGFCPGHPEDDOneofCase.CHFNLJPEENB) {
              subBuilder.MergeFrom(CHFNLJPEENB);
            }
            input.ReadMessage(subBuilder);
            CHFNLJPEENB = subBuilder;
            break;
          }
          case 16: {
            Status = (global::March7thHoney.Proto.PlanetFesQuestState) input.ReadEnum();
            break;
          }
          case 40: {
            Progress = input.ReadUInt32();
            break;
          }
          case 80: {
            QuestId = input.ReadUInt32();
            break;
          }
          case 88: {
            KGAABCGGHNM = input.ReadUInt32();
            break;
          }
          case 112: {
            OJBEACGLLOH = input.ReadUInt32();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            global::March7thHoney.Proto.EFEGKDHEMFN subBuilder = new global::March7thHoney.Proto.EFEGKDHEMFN();
            if (aGFCPGHPEDDCase_ == AGFCPGHPEDDOneofCase.CHFNLJPEENB) {
              subBuilder.MergeFrom(CHFNLJPEENB);
            }
            input.ReadMessage(subBuilder);
            CHFNLJPEENB = subBuilder;
            break;
          }
          case 16: {
            Status = (global::March7thHoney.Proto.PlanetFesQuestState) input.ReadEnum();
            break;
          }
          case 40: {
            Progress = input.ReadUInt32();
            break;
          }
          case 80: {
            QuestId = input.ReadUInt32();
            break;
          }
          case 88: {
            KGAABCGGHNM = input.ReadUInt32();
            break;
          }
          case 112: {
            OJBEACGLLOH = input.ReadUInt32();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
