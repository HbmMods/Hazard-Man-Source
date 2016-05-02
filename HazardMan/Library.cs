using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class Library
    {
        public static bool isSoundActivated = true;

        public static List<OptionPlayer> players = new List<OptionPlayer>();
        public static Dictionary<OptionPlayer, int> score = new Dictionary<OptionPlayer, int>();

        private static int level = 1;
        public static int getLevel() { return level; }
        public static void addLevel() { level++; }
        public static void setLevel(int nlevel) { level = nlevel; }

        public static bool recreateWorld = false;

        public static string s1 = "_//     _//      _/       _/////// _//      _/       _///////    _/////    ";
        public static string s2 = "_//     _//     _/ //            _//       _/ //     _//    _//  _//   _// ";
        public static string s3 = "_//     _//    _/  _//          _//       _/  _//    _//    _//  _//    _//";
        public static string s4 = "_////// _//   _//   _//       _//        _//   _//   _/ _//      _//    _//";
        public static string s5 = "_//     _//  _////// _//     _//        _////// _//  _//  _//    _//    _//";
        public static string s6 = "_//     _// _//       _//  _//         _//       _// _//    _//  _//   _// ";
        public static string s7 = "_//     _//_//         _//_///////////_//         _//_//      _//_/////    ";
        public static string s8 = " ";
        public static string s9 = "_//       _//      _/       _///     _//";
        public static string s10 = "_/ _//   _///     _/ //     _/ _//   _//";
        public static string s11 = "_// _// _ _//    _/  _//    _// _//  _//";
        public static string s12 = "_//  _//  _//   _//   _//   _//  _// _//";
        public static string s13 = "_//   _/  _//  _////// _//  _//   _/ _//";
        public static string s14 = "_//       _// _//       _// _//    _/ //";
        public static string s15 = "_//       _//_//         _//_//      _//";

        public static string option_name_00 = "    //   ) ) //   ) ) /__  ___/ / /    //   ) ) /|    / / //   ) ) ";
        public static string option_name_01 = "   //   / / //___/ /    / /    / /    //   / / //|   / / ((        ";
        public static string option_name_02 = "  //   / / / ____ /    / /    / /    //   / / // |  / /    \\      ";
        public static string option_name_03 = " //   / / //          / /    / /    //   / / //  | / /       ) )   ";
        public static string option_name_04 = "((___/ / //          / /  __/ /___ ((___/ / //   |/ / ((___ / /    ";
    }
}
