



#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace March7thHoney.Proto {

  
  public static partial class SecretKeyTypeReflection {

    #region Descriptor
    
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static SecretKeyTypeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNTZWNyZXRLZXlUeXBlLnByb3RvKnMKDVNlY3JldEtleVR5cGUSEwoPU0VD",
            "UkVUX0tFWV9OT05FEAASGwoXU0VDUkVUX0tFWV9TRVJWRVJfQ0hFQ0sQARIU",
            "ChBTRUNSRVRfS0VZX1ZJREVPEAISGgoWU0VDUkVUX0tFWV9CQVRUTEVfVElN",
            "RRADQhaqAhNNYXJjaDd0aEhvbmV5LlByb3RvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::March7thHoney.Proto.SecretKeyType), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum SecretKeyType {
    [pbr::OriginalName("SECRET_KEY_NONE")] SecretKeyNone = 0,
    [pbr::OriginalName("SECRET_KEY_SERVER_CHECK")] SecretKeyServerCheck = 1,
    [pbr::OriginalName("SECRET_KEY_VIDEO")] SecretKeyVideo = 2,
    [pbr::OriginalName("SECRET_KEY_BATTLE_TIME")] SecretKeyBattleTime = 3,
  }

  #endregion

}

#endregion Designer generated code
