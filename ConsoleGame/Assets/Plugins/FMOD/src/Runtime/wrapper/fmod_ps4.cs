#if UNITY_PS4
using System.Runtime.InteropServices;

namespace FMOD
{
    static class PS4
    {
        [global::System.Flags]
        public enum PORT_TYPE : uint
        {
            MUSIC,           /* Background music, pass FMOD_PORT_INDEX_NONE as port index */
            VOICE,           /* Voice chat, pass SceUserServiceUserId of desired user as port index */
            PERSONAL,        /* Personal audio device, pass SceUserServiceUserId of desired user as port index */
            CONTROLLER,      /* Controller speaker, pass SceUserServiceUserId of desired user as port index */
            COPYRIGHT_MUSIC, /* Copyright background music, pass FMOD_PORT_INDEX_NONE as port index. You cannot have both copyright and non-copyright music open simultaneously */
            SOCIAL,          /* VR only - social screen when in separate mode otherwise not output, pass FMOD_PORT_INDEX_NONE as port index. */
            MAX              /* Maximum number of port types supported. */
        }
    }
}
#endif
