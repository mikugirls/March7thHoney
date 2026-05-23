



#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace March7thHoney.Proto {

  
  public static partial class MissionStatusReflection {

    #region Descriptor
    
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static MissionStatusReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNNaXNzaW9uU3RhdHVzLnByb3RvKl4KDU1pc3Npb25TdGF0dXMSEAoMTUlT",
            "U0lPTl9OT05FEAASEQoNTUlTU0lPTl9ET0lORxABEhIKDk1JU1NJT05fRklO",
            "SVNIEAISFAoQTUlTU0lPTl9QUkVQQVJFRBADQhaqAhNNYXJjaDd0aEhvbmV5",
            "LlByb3RvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::March7thHoney.Proto.MissionStatus), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum MissionStatus {
    [pbr::OriginalName("MISSION_NONE")] MissionNone = 0,
    [pbr::OriginalName("MISSION_DOING")] MissionDoing = 1,
    [pbr::OriginalName("MISSION_FINISH")] MissionFinish = 2,
    [pbr::OriginalName("MISSION_PREPARED")] MissionPrepared = 3,
  }

  #endregion

}

#endregion Designer generated code
