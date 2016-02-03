using System;
using System.Collections.Generic;
using System.Text;

namespace TickTick.Utilities.FileUtility
{
    public class FileUtility
    {
        public static class FileType
        {

            private static string _image;

            public static string Image
            {
                get { return _image; }
                set { _image = value; }
            }
            private static string _voice;

            public static string Voice
            {
                get { return _voice; }
                set { _voice = value; }
            }
            private static string _other;

            public static string Other
            {
                get { return _other; }
                set { _other = value; }
            }
            private static string _audio;

            public static string Audio
            {
                get { return _audio; }
                set { _audio = value; }
            }

            public static string GetFileType(String name)
            {
                if (string.Equals(name, Image))
                {
                    return Image;
                }
                else if (string.Equals(name, Voice) || string.Equals(name, Voice))
                {
                    return Audio;
                }
                else
                {
                    return Other;
                }
            }

            public static bool IsAudio(string type)
            {
                return type == Voice || type == Audio;
            }

        }

    }
}
