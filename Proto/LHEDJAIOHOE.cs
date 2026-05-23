



#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace March7thHoney.Proto {

  
  public static partial class LHEDJAIOHOEReflection {

    #region Descriptor
    
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static LHEDJAIOHOEReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChFMSEVESkFJT0hPRS5wcm90byI3CgtMSEVESkFJT0hPRRITCgtOSkRCSUVN",
            "QU9BQRgIIAEoDRITCgtNQU9OTEhCS09GSxgKIAEoDUIWqgITTWFyY2g3dGhI",
            "b25leS5Qcm90b2IGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::March7thHoney.Proto.LHEDJAIOHOE), global::March7thHoney.Proto.LHEDJAIOHOE.Parser, new[]{ "NJDBIEMAOAA", "MAONLHBKOFK" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class LHEDJAIOHOE : pb::IMessage<LHEDJAIOHOE>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<LHEDJAIOHOE> _parser = new pb::MessageParser<LHEDJAIOHOE>(() => new LHEDJAIOHOE());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<LHEDJAIOHOE> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::March7thHoney.Proto.LHEDJAIOHOEReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LHEDJAIOHOE() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LHEDJAIOHOE(LHEDJAIOHOE other) : this() {
      nJDBIEMAOAA_ = other.nJDBIEMAOAA_;
      mAONLHBKOFK_ = other.mAONLHBKOFK_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LHEDJAIOHOE Clone() {
      return new LHEDJAIOHOE(this);
    }

    
    public const int NJDBIEMAOAAFieldNumber = 8;
    private uint nJDBIEMAOAA_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint NJDBIEMAOAA {
      get { return nJDBIEMAOAA_; }
      set {
        nJDBIEMAOAA_ = value;
      }
    }

    
    public const int MAONLHBKOFKFieldNumber = 10;
    private uint mAONLHBKOFK_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint MAONLHBKOFK {
      get { return mAONLHBKOFK_; }
      set {
        mAONLHBKOFK_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as LHEDJAIOHOE);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(LHEDJAIOHOE other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (NJDBIEMAOAA != other.NJDBIEMAOAA) return false;
      if (MAONLHBKOFK != other.MAONLHBKOFK) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (NJDBIEMAOAA != 0) hash ^= NJDBIEMAOAA.GetHashCode();
      if (MAONLHBKOFK != 0) hash ^= MAONLHBKOFK.GetHashCode();
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
      if (NJDBIEMAOAA != 0) {
        output.WriteRawTag(64);
        output.WriteUInt32(NJDBIEMAOAA);
      }
      if (MAONLHBKOFK != 0) {
        output.WriteRawTag(80);
        output.WriteUInt32(MAONLHBKOFK);
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
      if (NJDBIEMAOAA != 0) {
        output.WriteRawTag(64);
        output.WriteUInt32(NJDBIEMAOAA);
      }
      if (MAONLHBKOFK != 0) {
        output.WriteRawTag(80);
        output.WriteUInt32(MAONLHBKOFK);
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
      if (NJDBIEMAOAA != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(NJDBIEMAOAA);
      }
      if (MAONLHBKOFK != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(MAONLHBKOFK);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(LHEDJAIOHOE other) {
      if (other == null) {
        return;
      }
      if (other.NJDBIEMAOAA != 0) {
        NJDBIEMAOAA = other.NJDBIEMAOAA;
      }
      if (other.MAONLHBKOFK != 0) {
        MAONLHBKOFK = other.MAONLHBKOFK;
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
          case 64: {
            NJDBIEMAOAA = input.ReadUInt32();
            break;
          }
          case 80: {
            MAONLHBKOFK = input.ReadUInt32();
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
          case 64: {
            NJDBIEMAOAA = input.ReadUInt32();
            break;
          }
          case 80: {
            MAONLHBKOFK = input.ReadUInt32();
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
