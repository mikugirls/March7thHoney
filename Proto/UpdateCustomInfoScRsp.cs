



#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace March7thHoney.Proto {

  
  public static partial class UpdateCustomInfoScRspReflection {

    #region Descriptor
    
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static UpdateCustomInfoScRspReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChtVcGRhdGVDdXN0b21JbmZvU2NSc3AucHJvdG8iXwoVVXBkYXRlQ3VzdG9t",
            "SW5mb1NjUnNwEg8KB3JldGNvZGUYAiABKA0SDwoFdGl0bGUYBSABKAlIABIV",
            "CgtGQ09LT0dOT0dDQRgLIAEoDUgAQg0KC05MUFBQQ0hHR1BGQhaqAhNNYXJj",
            "aDd0aEhvbmV5LlByb3RvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::March7thHoney.Proto.UpdateCustomInfoScRsp), global::March7thHoney.Proto.UpdateCustomInfoScRsp.Parser, new[]{ "Retcode", "Title", "FCOKOGNOGCA" }, new[]{ "NLPPPCHGGPF" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class UpdateCustomInfoScRsp : pb::IMessage<UpdateCustomInfoScRsp>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<UpdateCustomInfoScRsp> _parser = new pb::MessageParser<UpdateCustomInfoScRsp>(() => new UpdateCustomInfoScRsp());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<UpdateCustomInfoScRsp> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::March7thHoney.Proto.UpdateCustomInfoScRspReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public UpdateCustomInfoScRsp() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public UpdateCustomInfoScRsp(UpdateCustomInfoScRsp other) : this() {
      retcode_ = other.retcode_;
      switch (other.NLPPPCHGGPFCase) {
        case NLPPPCHGGPFOneofCase.Title:
          Title = other.Title;
          break;
        case NLPPPCHGGPFOneofCase.FCOKOGNOGCA:
          FCOKOGNOGCA = other.FCOKOGNOGCA;
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public UpdateCustomInfoScRsp Clone() {
      return new UpdateCustomInfoScRsp(this);
    }

    
    public const int RetcodeFieldNumber = 2;
    private uint retcode_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint Retcode {
      get { return retcode_; }
      set {
        retcode_ = value;
      }
    }

    
    public const int TitleFieldNumber = 5;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Title {
      get { return HasTitle ? (string) nLPPPCHGGPF_ : ""; }
      set {
        nLPPPCHGGPF_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
        nLPPPCHGGPFCase_ = NLPPPCHGGPFOneofCase.Title;
      }
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasTitle {
      get { return nLPPPCHGGPFCase_ == NLPPPCHGGPFOneofCase.Title; }
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearTitle() {
      if (HasTitle) {
        ClearNLPPPCHGGPF();
      }
    }

    
    public const int FCOKOGNOGCAFieldNumber = 11;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint FCOKOGNOGCA {
      get { return HasFCOKOGNOGCA ? (uint) nLPPPCHGGPF_ : 0; }
      set {
        nLPPPCHGGPF_ = value;
        nLPPPCHGGPFCase_ = NLPPPCHGGPFOneofCase.FCOKOGNOGCA;
      }
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasFCOKOGNOGCA {
      get { return nLPPPCHGGPFCase_ == NLPPPCHGGPFOneofCase.FCOKOGNOGCA; }
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearFCOKOGNOGCA() {
      if (HasFCOKOGNOGCA) {
        ClearNLPPPCHGGPF();
      }
    }

    private object nLPPPCHGGPF_;
    
    public enum NLPPPCHGGPFOneofCase {
      None = 0,
      Title = 5,
      FCOKOGNOGCA = 11,
    }
    private NLPPPCHGGPFOneofCase nLPPPCHGGPFCase_ = NLPPPCHGGPFOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public NLPPPCHGGPFOneofCase NLPPPCHGGPFCase {
      get { return nLPPPCHGGPFCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearNLPPPCHGGPF() {
      nLPPPCHGGPFCase_ = NLPPPCHGGPFOneofCase.None;
      nLPPPCHGGPF_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as UpdateCustomInfoScRsp);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(UpdateCustomInfoScRsp other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Retcode != other.Retcode) return false;
      if (Title != other.Title) return false;
      if (FCOKOGNOGCA != other.FCOKOGNOGCA) return false;
      if (NLPPPCHGGPFCase != other.NLPPPCHGGPFCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Retcode != 0) hash ^= Retcode.GetHashCode();
      if (HasTitle) hash ^= Title.GetHashCode();
      if (HasFCOKOGNOGCA) hash ^= FCOKOGNOGCA.GetHashCode();
      hash ^= (int) nLPPPCHGGPFCase_;
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
      if (Retcode != 0) {
        output.WriteRawTag(16);
        output.WriteUInt32(Retcode);
      }
      if (HasTitle) {
        output.WriteRawTag(42);
        output.WriteString(Title);
      }
      if (HasFCOKOGNOGCA) {
        output.WriteRawTag(88);
        output.WriteUInt32(FCOKOGNOGCA);
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
      if (Retcode != 0) {
        output.WriteRawTag(16);
        output.WriteUInt32(Retcode);
      }
      if (HasTitle) {
        output.WriteRawTag(42);
        output.WriteString(Title);
      }
      if (HasFCOKOGNOGCA) {
        output.WriteRawTag(88);
        output.WriteUInt32(FCOKOGNOGCA);
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
      if (Retcode != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Retcode);
      }
      if (HasTitle) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Title);
      }
      if (HasFCOKOGNOGCA) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(FCOKOGNOGCA);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(UpdateCustomInfoScRsp other) {
      if (other == null) {
        return;
      }
      if (other.Retcode != 0) {
        Retcode = other.Retcode;
      }
      switch (other.NLPPPCHGGPFCase) {
        case NLPPPCHGGPFOneofCase.Title:
          Title = other.Title;
          break;
        case NLPPPCHGGPFOneofCase.FCOKOGNOGCA:
          FCOKOGNOGCA = other.FCOKOGNOGCA;
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
          case 16: {
            Retcode = input.ReadUInt32();
            break;
          }
          case 42: {
            Title = input.ReadString();
            break;
          }
          case 88: {
            FCOKOGNOGCA = input.ReadUInt32();
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
          case 16: {
            Retcode = input.ReadUInt32();
            break;
          }
          case 42: {
            Title = input.ReadString();
            break;
          }
          case 88: {
            FCOKOGNOGCA = input.ReadUInt32();
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
