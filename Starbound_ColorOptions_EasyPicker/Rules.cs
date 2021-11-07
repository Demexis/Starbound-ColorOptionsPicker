using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbound_ColorOptions_EasyPicker
{
    public class Rules
    {
        public const int BitmapSizeDefault = 43;

        public enum ColorOptions { Default, Black, Grey, White, Red, Orange, Yellow, Green, Blue, Purple, Pink, Brown }
        public enum Sex { Male, Female }

        public readonly static Dictionary<string, int> PosesNNumOfFrames = new Dictionary<string, int>()
        {
            ["Idle"] = 5,
            ["Duck"] = 1,
            ["Walk"] = 8,
            ["Run"] = 8,
            ["Jump"] = 8,
            ["Swim"] = 4
        };

        public readonly static Dictionary<string, Dictionary<string, Point[]>> PosesNCoordinates = new Dictionary<string, Dictionary<string, Point[]>>()
        {
            ["back"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 2, Y = 0 },
                    new Point() { X = 3, Y = 0 },
                    new Point() { X = 4, Y = 0 },
                    new Point() { X = 5, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 8, Y = 0 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 1, Y = 1 },
                    new Point() { X = 2, Y = 1 },
                    new Point() { X = 3, Y = 1 },
                    new Point() { X = 4, Y = 1 },
                    new Point() { X = 5, Y = 1 },
                    new Point() { X = 6, Y = 1 },
                    new Point() { X = 7, Y = 1 },
                    new Point() { X = 8, Y = 1 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 2, Y = 2 },
                    new Point() { X = 3, Y = 2 },
                    new Point() { X = 4, Y = 2 },
                    new Point() { X = 5, Y = 2 },
                    new Point() { X = 6, Y = 2 },
                    new Point() { X = 7, Y = 2 },
                    new Point() { X = 8, Y = 2 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 1, Y = 3 },
                    new Point() { X = 2, Y = 3 },
                    new Point() { X = 3, Y = 3 },
                    new Point() { X = 4, Y = 3 },
                    new Point() { X = 5, Y = 3 },
                    new Point() { X = 6, Y = 3 },
                    new Point() { X = 7, Y = 3 },
                    new Point() { X = 8, Y = 3 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 4, Y = 5 },
                    new Point() { X = 5, Y = 5 },
                    new Point() { X = 6, Y = 5 },
                    new Point() { X = 7, Y = 5 }
                }
            },
            ["Bsleeve"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 2, Y = 0 },
                    new Point() { X = 3, Y = 0 },
                    new Point() { X = 4, Y = 0 },
                    new Point() { X = 5, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 8, Y = 0 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 3, Y = 1 },
                    new Point() { X = 2, Y = 1 },
                    new Point() { X = 3, Y = 1 },
                    new Point() { X = 4, Y = 1 },
                    new Point() { X = 5, Y = 1 },
                    new Point() { X = 6, Y = 1 },
                    new Point() { X = 5, Y = 1 },
                    new Point() { X = 4, Y = 1 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 3, Y = 2 },
                    new Point() { X = 2, Y = 2 },
                    new Point() { X = 3, Y = 2 },
                    new Point() { X = 4, Y = 2 },
                    new Point() { X = 5, Y = 2 },
                    new Point() { X = 6, Y = 2 },
                    new Point() { X = 5, Y = 2 },
                    new Point() { X = 4, Y = 2 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 1, Y = 3 },
                    new Point() { X = 2, Y = 3 },
                    new Point() { X = 3, Y = 3 },
                    new Point() { X = 4, Y = 3 },
                    new Point() { X = 5, Y = 3 },
                    new Point() { X = 6, Y = 3 },
                    new Point() { X = 7, Y = 3 },
                    new Point() { X = 8, Y = 3 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 4, Y = 5 },
                    new Point() { X = 5, Y = 5 },
                    new Point() { X = 6, Y = 5 },
                    new Point() { X = 7, Y = 5 }
                }
            },
            ["Fsleeve"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 2, Y = 0 },
                    new Point() { X = 3, Y = 0 },
                    new Point() { X = 4, Y = 0 },
                    new Point() { X = 5, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 8, Y = 0 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 3, Y = 1 },
                    new Point() { X = 2, Y = 1 },
                    new Point() { X = 3, Y = 1 },
                    new Point() { X = 4, Y = 1 },
                    new Point() { X = 5, Y = 1 },
                    new Point() { X = 6, Y = 1 },
                    new Point() { X = 5, Y = 1 },
                    new Point() { X = 4, Y = 1 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 3, Y = 2 },
                    new Point() { X = 2, Y = 2 },
                    new Point() { X = 3, Y = 2 },
                    new Point() { X = 4, Y = 2 },
                    new Point() { X = 5, Y = 2 },
                    new Point() { X = 6, Y = 2 },
                    new Point() { X = 5, Y = 2 },
                    new Point() { X = 4, Y = 2 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 1, Y = 3 },
                    new Point() { X = 2, Y = 3 },
                    new Point() { X = 3, Y = 3 },
                    new Point() { X = 4, Y = 3 },
                    new Point() { X = 5, Y = 3 },
                    new Point() { X = 6, Y = 3 },
                    new Point() { X = 7, Y = 3 },
                    new Point() { X = 8, Y = 3 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 4, Y = 5 },
                    new Point() { X = 5, Y = 5 },
                    new Point() { X = 6, Y = 5 },
                    new Point() { X = 7, Y = 5 }
                }
            },
            ["pants"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 2, Y = 0 },
                    new Point() { X = 3, Y = 0 },
                    new Point() { X = 4, Y = 0 },
                    new Point() { X = 5, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 8, Y = 0 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 1, Y = 1 },
                    new Point() { X = 2, Y = 1 },
                    new Point() { X = 3, Y = 1 },
                    new Point() { X = 4, Y = 1 },
                    new Point() { X = 5, Y = 1 },
                    new Point() { X = 6, Y = 1 },
                    new Point() { X = 7, Y = 1 },
                    new Point() { X = 8, Y = 1 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 2, Y = 2 },
                    new Point() { X = 3, Y = 2 },
                    new Point() { X = 4, Y = 2 },
                    new Point() { X = 5, Y = 2 },
                    new Point() { X = 6, Y = 2 },
                    new Point() { X = 7, Y = 2 },
                    new Point() { X = 8, Y = 2 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 1, Y = 3 },
                    new Point() { X = 2, Y = 3 },
                    new Point() { X = 3, Y = 3 },
                    new Point() { X = 4, Y = 3 },
                    new Point() { X = 5, Y = 3 },
                    new Point() { X = 6, Y = 3 },
                    new Point() { X = 7, Y = 3 },
                    new Point() { X = 8, Y = 3 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 4, Y = 5 },
                    new Point() { X = 5, Y = 5 },
                    new Point() { X = 6, Y = 5 },
                    new Point() { X = 7, Y = 5 }
                }
            },
            ["chestm"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 1, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 1, Y = 3 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 1, Y = 5 },
                    new Point() { X = 1, Y = 5 },
                    new Point() { X = 1, Y = 5 },
                    new Point() { X = 1, Y = 5 }
                }
            },
            ["chestf"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 1, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 1, Y = 3 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 1, Y = 5 },
                    new Point() { X = 1, Y = 5 },
                    new Point() { X = 1, Y = 5 },
                    new Point() { X = 1, Y = 5 }
                }
            },
            ["chest"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 1, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 1, Y = 3 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 },
                    new Point() { X = 1, Y = 2 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 1, Y = 5 },
                    new Point() { X = 1, Y = 5 },
                    new Point() { X = 1, Y = 5 },
                    new Point() { X = 1, Y = 5 }
                }
            },
            ["head"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 1, Y = 0 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                }
            }
        };

        // OFFSETS
        public readonly static Dictionary<string, Dictionary<string, Point[]>> PosesNOffsets = new Dictionary<string, Dictionary<string, Point[]>>()
        {
            ["back"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 0, Y = 8 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 2 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 2 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 2 },
                    new Point() { X = 0, Y = 1 }
                }
            },
            ["Bsleeve"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 0, Y = 8 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = -2, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 2 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 2 },
                    new Point() { X = 0, Y = 1 }
                }
            },
            ["Fsleeve"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 0, Y = 8 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 2 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 2 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 2 },
                    new Point() { X = 0, Y = 1 }
                }
            },
            ["pants"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 0, Y = 8 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 2 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 2 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 },
                    new Point() { X = 0, Y = -1 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 2 },
                    new Point() { X = 0, Y = 1 }
                }
            },
            ["chestm"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 0, Y = 8 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 }
                }
            },
            ["chestf"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 0, Y = 8 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 }
                }
            },
            ["chest"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 0, Y = 8 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 }
                }
            },
            ["head"] = new Dictionary<string, Point[]>
            {
                ["Idle"] = new Point[5]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = -1, Y = 1 },
                    new Point() { X = -1, Y = 1 },
                    new Point() { X = -1, Y = 1 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Duck"] = new Point[1]
                {
                    new Point() { X = 0, Y = 0 }
                },
                ["Walk"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 1 }
                },
                ["Run"] = new Point[8]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                },
                ["Jump"] = new Point[8]
                {
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 },
                    new Point() { X = 0, Y = 0 }
                },
                ["Swim"] = new Point[4]
                {
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 },
                    new Point() { X = 1, Y = 0 }
                }
            }
        };

    }
}
