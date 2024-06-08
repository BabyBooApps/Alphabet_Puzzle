using UnityEngine;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System;

public class DataFeed : MonoBehaviour
{
    public static DataTable data_table = new DataTable();

	public static List<int> matchingList = new List<int>();
	//public static float aspect = 0f;

	public static string[] Attributes = new string[] { "LETTER", "UPPERCASE", "UPPERCASE_SHADOW",  "LOWERCASE", "LOWERCASE_SHADOW", "CARD_1", "CARD_2",
        "ALPHABET_CLIP_FEMALE", "ALPHABET_CLIP_MALE", "CARD_1_AUDIO", "CARD_2_AUDIO", "TRACING_UPPERCASE_WHITE","TRACING_UPPERCASE_COLOR","POSITIONS_UPPERCASE" };
	      
	public static string[] alphabets = new string[]{"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};


    #region DotPositionsArrays
    public static Vector3[][] A = new Vector3[][] {
        new Vector3[] {new Vector3(0.04f,3.73f,10f),new Vector3(-0.34f,2.77f,10f),new Vector3(-0.74f,1.75f,10f),new Vector3(-1.12f,0.8f,10f),new Vector3(-1.5f,-0.15f,10f),new Vector3(-1.91f,-1.18f,10f)},
        new Vector3[] {new Vector3(0.04f,3.73f,10f),new Vector3(0.42f,2.77f,10f),new Vector3(0.83f,1.75f,10f),new Vector3(1.22f,0.8f,10f),new Vector3(1.60f,-0.14f,10f),new Vector3(2f,-1.18f,10f)},
        new Vector3[] {new Vector3(-1.52f,-0.15f,10f),new Vector3(-0.46f,-0.15f,10f),new Vector3(0.56f,-0.15f,10f),new Vector3(1.60f,-0.15f,10f)}
    };

    public static Vector3[][] B = new Vector3[][] {
        new Vector3[] {new Vector3(-1.27f,3.52f,10f),new Vector3(-1.27f,2.33f,10f),new Vector3(-1.27f,1.17f,10f),new Vector3(-1.27f,-0.06f,10f),new Vector3(-1.27f,-1.34f,10f)},
        new Vector3[] {new Vector3(-1.27f,3.52f,10f),new Vector3(-0.55f,3.52f,10f),new Vector3(0.11f,3.4f,10f),new Vector3(0.7f,3f,10f),new Vector3(0.83f,2.33f,10f),new Vector3(0.6f,1.67f,10f),new Vector3(0.11f,1.26f,10f),new Vector3(-0.63f,1.17f,10f),new Vector3(-1.27f,1.17f,10f)},
        new Vector3[] {new Vector3(0.11f,1.26f,10f),new Vector3(0.86f,0.78f,10f),new Vector3(1.3f,-0.05f,10f),new Vector3(1.07f,-0.8f,10f),new Vector3(0.42f,-1.31f,10f),new Vector3(-0.45f,-1.41f,10f),new Vector3(-1.27f,-1.34f,10f)}
    };

    public static Vector3[][] C = new Vector3[][] {
        new Vector3[] {new Vector3(1.99f,2.99f,10f),new Vector3(0.94f,3.52f,10f),new Vector3(-0.08f,3.57f,10f),new Vector3(-1.07f,3.18f,10f),new Vector3(-1.93f,2.31f,10f),new Vector3(-2.22f,1.10f,10f),new Vector3(-1.93f,-0.12f,10f),new Vector3(-1.07f,-1.04f,10f),new Vector3(-0.08f,-1.42f,10f),new Vector3(0.94f,-1.39f,10f),new Vector3(1.99f,-0.98f,10f)}
    };

    public static Vector3[][] D = new Vector3[][] {
        new Vector3[] {new Vector3(-1.79f,3.49f,10f),new Vector3(-1.79f,2.27f,10f),new Vector3(-1.79f,1.03f,10f),new Vector3(-1.79f,-0.16f,10f),new Vector3(-1.79f,-1.37f,10f)},
        new Vector3[] {new Vector3(-1.79f,3.49f,10f),new Vector3(-0.55f,3.49f,10f),new Vector3(0.68f,3.22f,10f),new Vector3(1.58f,2.24f,10f),new Vector3(1.80f,1.02f,10f),new Vector3(1.58f,-0.31f,10f),new Vector3(0.68f,-1.15f,10f),new Vector3(-0.55f,-1.37f,10f),new Vector3(-1.79f,-1.37f,10f)}
    };

    public static Vector3[][] E = new Vector3[][] {
        new Vector3[] {new Vector3(-1.02F,3.52F,10f),new Vector3(-1.02f,2.41f,10f),new Vector3(-1.02f,1.25f,10f),new Vector3(-1.02f,0f,10f),new Vector3(-1.02f,-1.39f,10f)},
        new Vector3[] {new Vector3(-1.02f,3.52f,10f),new Vector3(0.12f,3.52f,10f),new Vector3(1.24f,3.52f,10f)},
        new Vector3[] {new Vector3(-1.02f,1.25f,10f),new Vector3(0.12f,1.25f,10f),new Vector3(1.24f,1.25f,10f)},
        new Vector3[] {new Vector3(-1.03f,-1.39f,10f),new Vector3(0.12f,-1.39f,10f),new Vector3(1.25f,-1.39f,10f)}
		//new Vector3[] {new Vector3(-1.02f,3.52f,10f),new Vector3(-0.28f,3.52f,10f),new Vector3(0.517f,3.52f,10f),new Vector3(1.24f,3.52f,10f)},
		//new Vector3[] {new Vector3(-1.02f,1.25f,10f),new Vector3(-0.28f,1.25f,10f),new Vector3(0.52f,1.25f,10f),new Vector3(1.24f,1.25f,10f)},
		//new Vector3[] {new Vector3(-1.03f,-1.39f,10f),new Vector3(-0.28f,-1.39f,10f),new Vector3(0.52f,-1.39f,10f),new Vector3(1.25f,-1.39f,10f)}
	};

    public static Vector3[][] F = new Vector3[][] {
        new Vector3[] {new Vector3(-0.86f,3.51f,10f),new Vector3(-0.86f,2.38f,10f),new Vector3(-0.86f,1.23f,10f),new Vector3(-0.86f,-0.15f,10f),new Vector3(-0.86f,-1.46f,10f)},
		//new Vector3[] {new Vector3(-0.86f,3.51f,10f),new Vector3(-0.22f,3.51f,10f),new Vector3(0.45f,3.51f,10f),new Vector3(1.16f,3.51f,10f)},
		new Vector3[] {new Vector3(-0.86f,3.51f,10f),new Vector3(0.15f,3.51f,10f),new Vector3(1.16f,3.51f,10f)},
		//new Vector3[] {new Vector3(-0.86f,1.23f,10f),new Vector3(-0.21f,1.23f,10f),new Vector3(0.46f,1.23f,10f),new Vector3(1.17f,1.23f,10f)}
		new Vector3[] {new Vector3(-0.86f,1.23f,10f),new Vector3(0.15f,1.23f,10f),new Vector3(1.17f,1.23f,10f)}
    };

    public static Vector3[][] G = new Vector3[][] {
        new Vector3[] {new Vector3(1.82f,3.01f,10f),new Vector3(0.73f,3.53f,10f),new Vector3(-0.57f,3.53f,10f),new Vector3(-1.6f,2.99f,10f),new Vector3(-2.26f,1.97f,10f),new Vector3(-2.42f,0.77f,10f),new Vector3(-1.91f,-0.46f,10f),new Vector3(-0.94f,-1.22f,10f),new Vector3(0.24f,-1.44f,10f),new Vector3(1.32f,-1.17f,10f),new Vector3(2.11f,-0.42f,10f),new Vector3(2.54f,0.55f,10f)},
        new Vector3[] {new Vector3(0.59f,0.55f,10f),new Vector3(2.54f,0.55f,10f)}
    };

    public static Vector3[][] H = new Vector3[][] {
        new Vector3[] {new Vector3(-1.54f,3.51f,10f),new Vector3(-1.54f,2.311f,10f),new Vector3(-1.54f,1.22f,10f),new Vector3(-1.54f,-0.07f,10f),new Vector3(-1.54f,-1.42f,10f)},
        new Vector3[] {new Vector3(1.59f,3.51f,10f),new Vector3(1.59f,2.31f,10f),new Vector3(1.59f,1.22f,10f),new Vector3(1.59f,-0.07f,10f),new Vector3(1.59f,-1.42f,10f)},
        new Vector3[] {new Vector3(-1.54f,1.22f,10f),new Vector3(-0.55f,1.22f,10f),new Vector3(0.57f,1.22f,10f),new Vector3(1.59f,1.22f,10f)}
    };

    public static Vector3[][] I = new Vector3[][] {
        new Vector3[] {new Vector3(0.02f,3.51f,10f),new Vector3(0.02f,2.33f,10f),new Vector3(0.02f,1.1f,10f),new Vector3(0.02f,-0.20f,10f),new Vector3(0.02f,-1.51f,10f)}
    };

    public static Vector3[][] J = new Vector3[][] {
        new Vector3[] {new Vector3(1.01f,3.72f,10f),new Vector3(1.01f,2.68f,10f),new Vector3(1.01f,1.6f,10f),new Vector3(1.01f,0.55f,10f),new Vector3(1.01f,-0.44f,10f),new Vector3(0.65f,-1.23f,10f),new Vector3(-0.08f,-1.46f,10f),new Vector3(-0.78f,-1.17f,10f)}
    };

    public static Vector3[][] K = new Vector3[][] {
        new Vector3[] {new Vector3(-1.52f,3.7f,10f),new Vector3(-1.52f,2.3f,10f),new Vector3(-1.52f,1f,10f),new Vector3(-1.52f,-0.3f,10f),new Vector3(-1.52f,-1.63f,10f)},
        new Vector3[] {new Vector3(1.05f,3.7f,10f),new Vector3(0.47f,2.96f,10f),new Vector3(-0.125f,2.2f,10f),new Vector3(-0.72f,1.44f,10f),new Vector3(-1.52f,0.43f,10f)},
        new Vector3[] {new Vector3(-0.76f,1.44f,10f),new Vector3(-0.13f,0.49f,10f),new Vector3(0.52f,-0.49f,10f),new Vector3(1.25f,-1.55f,10f)}

    };

    public static Vector3[][] L = new Vector3[][] {
        new Vector3[] {new Vector3(-0.77f,3.64f,10f),new Vector3(-0.77f,2.37f,10f),new Vector3(-0.77f,1.14f,10f),new Vector3(-0.77f,-0.19f,10f),new Vector3(-0.77f,-1.36f,10f)},
        new Vector3[] {new Vector3(-0.77f,-1.36f,10f),new Vector3(0.10f,-1.36f,10f),new Vector3(0.95f,-1.36f,10f)}
    };

    public static Vector3[][] M = new Vector3[][] {
        new Vector3[] {new Vector3(-1.98f,3.69f,10f),new Vector3(-2.14f,2.74f,10f),new Vector3(-2.33f,1.67f,10f),new Vector3(-2.53f,0.54f,10f),new Vector3(-2.71f,-0.43f,10f),new Vector3(-2.91f,-1.55f,10f)},

        new Vector3[] {new Vector3(1.98f,3.69f,10f),new Vector3(2.148f,2.74f,10f),new Vector3(2.336f,1.67f,10f),new Vector3(2.536f,0.54f,10f),new Vector3(2.706f,-0.43f,10f),new Vector3(2.9f,-1.53f,10f)},

        new Vector3[] {new Vector3(-1.98f, 3.69f, 10f),new Vector3(-1.61f,2.73f,10f),new Vector3(-1.20f,1.65f,10f),new Vector3(-0.83f,0.64f,10f),new Vector3(-0.42f,-0.41f,10f),new Vector3(-0.02f,-1.54f,10f)},

        new Vector3[] {new Vector3(1.98f, 3.69f, 10f),new Vector3(1.60f, 2.73f, 10f), new Vector3(1.22f, 1.69f, 10f), new Vector3(0.81f, 0.64f, 10f), new Vector3(0.41f, -0.39f, 10f), new Vector3(-0.02f,-1.54f,10f)}
		
		/*new Vector3[] {new Vector3(-0.02f,-1.54f,10f), new Vector3(0.41f, -0.39f, 10f), new Vector3(0.81f, 0.64f, 10f), new Vector3(1.22f, 1.69f, 10f), new Vector3(1.60f, 2.73f, 10f), new Vector3(1.98f, 3.69f, 10f) }*/
	};

    public static Vector3[][] N = new Vector3[][] {
        new Vector3[] { new Vector3(-1.79f, 3.75f, 10f),new Vector3(-1.79f, 2.68f, 10f),new Vector3(-1.79f, 1.6f, 10f),new Vector3(-1.79f, 0.43f, 10f),new Vector3(-1.79f, -0.56f, 10f),new Vector3(-1.79f, -1.61f, 10f) },
        new Vector3[] { new Vector3(1.87f, 3.76f, 10f),new Vector3(1.87f, 2.71f, 10f),new Vector3(1.87f, 1.6f, 10f),new Vector3(1.87f, 0.44f, 10f),new Vector3(1.87f, -0.56f, 10f),new Vector3(1.87f, -1.6f, 10f) },
        new Vector3[] { new Vector3(-1.79f, 3.75f,10f),new Vector3(-1.05f, 2.68f,10f),new Vector3(-0.33f, 1.6f,10f),new Vector3(0.48f, 0.41f,10f),new Vector3(1.13f, -0.56f,10f), new Vector3(1.84f, -1.6f, 10f) },

    };

    public static Vector3[][] O = new Vector3[][] {
        new Vector3[] {new Vector3(0f,3.58f,10f),new Vector3(-1.19f,3.22f,10f),new Vector3(-2.13f,2.28f,10f),new Vector3(-2.42f,1.17f,10f),new Vector3(-2.22f,0.01f,10f),new Vector3(-1.48f,-0.96f,10f),new Vector3(0f,-1.44f,10f),new Vector3(1.48f,-0.97f,10f),new Vector3(2.26f,0.03f,10f),new Vector3(2.47f,1.17f,10f),new Vector3(2.14f,2.30f,10f),new Vector3(1.19f,3.21f,10f),new Vector3(0f,3.58f,10f)}
    };

    public static Vector3[][] P = new Vector3[][] {
        new Vector3[] {new Vector3(-1.25f, 3.63f,10f),new  Vector3(-1.25f, 2.45f,10f),new Vector3(-1.25f, 1.26f,10f),new Vector3(-1.26f, -0.1f,10f),new Vector3(-1.26f, -1.51f,10f) },
        new Vector3[] {new Vector3(-1.25f, 3.63f,10f),new Vector3(-0.34f, 3.63f,10f),new Vector3(0.54f, 3.59f,10f), new Vector3(1.07f, 3.06f,10f),new Vector3(1.33f, 2.43f,10f),new Vector3(1.08f, 1.77f,10f),new Vector3(0.53f, 1.34f,10f),new Vector3(-0.34f, 1.26f,10f),new Vector3(-1.25f, 1.26f,10f) },
    };

    public static Vector3[][] Q = new Vector3[][] {
        new Vector3[] {new Vector3(-0.01f, 3.74f,10f),new Vector3(-1.28f, 3.37f,10f),new Vector3(-2.08f, 2.63f,10f),new Vector3(-2.53f, 1.56f,10f),new Vector3(-2.43f, 0.45f,10f),new Vector3(-1.77f, -0.64f,10f),new Vector3(-0.80f, -1.23f,10f),new Vector3(0.36f, -1.22f,10f), new Vector3(1.46f, -0.73f,10f),new Vector3(2.28f, 0.29f,10f),new Vector3(2.43f, 1.46f,10f),new Vector3(2.06f, 2.57f,10f),new Vector3(1.27f, 3.36f,10f),new Vector3(-0.01f, 3.74f,10f) },
        new Vector3[] {new Vector3(0.55f, 0.43f,10f),new Vector3(0.98f, -0.09f,10f),new Vector3(1.46f, -0.73f,10f),new Vector3(1.85f, -1.26f,10f),new Vector3(2.33f,-1.88f,10f) }
    };

    public static Vector3[][] R = new Vector3[][] {
        new Vector3[] {new Vector3(-1.28f, 3.68f,10f),new Vector3(-1.28f, 2.52f,10f),new Vector3(-1.28f, 1.19f,10f),new Vector3(-1.28f, -0.16f,10f),new Vector3(-1.28f, -1.5f,10f) },
        new Vector3[] { new Vector3(-1.28f, 3.68f,10f),new Vector3(-0.46f, 3.67f,10f),new Vector3(0.44f, 3.42f,10f),new Vector3(1f, 2.79f,10f),new Vector3(1.03f, 1.93f,10f),new Vector3(0.49f, 1.43f,10f),new Vector3(-0.44f, 1.21f,10f),new Vector3(-1.29f, 1.19f,10f)},
        new Vector3[] { new Vector3(-0.44f, 1.21f,10f), new Vector3(0.14f, 0.10f,10f), new Vector3(0.56f, -0.65f,10f),new Vector3(1.11f,-1.58f,10f) }
    };

    public static Vector3[][] S = new Vector3[][] {
        new Vector3[] {new Vector3(1.12f, 3.13f,10f),new Vector3(0.34f, 3.59f,10f),new Vector3(-0.57f, 3.42f,10f),new Vector3(-0.98f, 2.74f,10f),new Vector3(-0.63f, 2.01f,10f),new Vector3(-0.03f, 1.34f,10f),new Vector3(0.68f, 0.73f,10f),new Vector3(1.21f, 0f,10f),new Vector3(1.18f, -0.94f,10f),new Vector3(0.53f, -1.45f,10f),new Vector3 (-0.51f, -1.4f,10f),new Vector3(-1.19f, -0.82f,10f) },
    };

    public static Vector3[][] T = new Vector3[][] {
        new Vector3[] {new Vector3(0f,3.52f,10f),new Vector3(0f, 2.25f,10f),new Vector3(0f, 0.98f,10f),new Vector3(0f, -0.26f,10f),new Vector3(0f, -1.58f,10f) },
        new Vector3[] {new Vector3(-1.39f, 3.52f,10f),/*new Vector3(-0.72f, 3.52f,10f),*/new Vector3(0.005f, 3.52f,10f),/* new Vector3(0.72f, 3.52f,10f),*/new Vector3(1.45f, 3.52f,10f) }
    };

    public static Vector3[][] U = new Vector3[][] {
          new Vector3[] {new Vector3(-1.38f,3.75f,10f),new Vector3(-1.38f,2.82f,10f),new Vector3(-1.38f,1.88f,10f),new Vector3(-1.38f,0.88f,10f),new Vector3(-1.38f,-0.1f,10f),new Vector3(-1.06f,-1.05f,10f),new Vector3(0.02f,-1.45f,10f),new Vector3(1.06f,-1.05f,10f),new Vector3(1.45f,-0.1f,10f),new Vector3(1.45f,0.9f,10f),new Vector3(1.45f,1.87f,10f),new Vector3(1.45f,2.82f,10f),new Vector3(1.45f,3.77f,10f)}
    };

    public static Vector3[][] V = new Vector3[][] {
        new Vector3[] {new Vector3(-2.007f, 3.69f,10f),new Vector3(-1.47f, 2.33f,10f),new Vector3(-0.96f, 1.06f,10f),new Vector3(-0.48f, -0.17f,10f),new Vector3(0.00f, -1.39f,10f) },
        new Vector3[] {new Vector3(1.99f, 3.69f,10f),new Vector3(1.46f, 2.33f,10f),new Vector3(0.96f, 1.06f,10f),new Vector3(0.48f, -0.17f,10f), new Vector3(0.00f, -1.39f,10f) }

    };

    public static Vector3[][] W = new Vector3[][] {
        new Vector3[] {new Vector3(-2.93f,3.76f,10f),new Vector3(-2.59f,2.42f,10f),new Vector3(-2.24f,1.09f,10f),new Vector3(-1.92f,-0.23f,10f),new Vector3(-1.64f,-1.41f,10f)},
        new Vector3[] {new Vector3(-0.03f, 3.76f, 10f),new Vector3(-0.43f, 2.42f, 10f),new Vector3(-0.84f, 1.09f, 10f),new Vector3(-1.25f, -0.23f, 10f),new Vector3(-1.64f, -1.41f, 10f) },
        new Vector3[] {new Vector3(-0.03f,3.76f,10f),new Vector3(0.44f,2.42f,10f),new Vector3(0.85f,1.09f,10f),new Vector3(1.27f,-0.23f,10f),new Vector3(1.66f,-1.41f,10f)},
        new Vector3[] { new Vector3(3.01f, 3.76f, 10f), new Vector3(2.66f, 2.42f, 10f), new Vector3(2.31f, 1.09f, 10f), new Vector3(1.97f, -0.23f, 10f), new Vector3(1.66f, -1.41f, 10f) }
   };

    public static Vector3[][] X = new Vector3[][] {
        new Vector3[] {new Vector3(-1.68f, 3.72f,10f),new Vector3(-1.16f, 2.93f,10f),new Vector3(-0.546f, 1.99f,10f), new Vector3(0f, 1.14f,10f),new Vector3(0.64f, 0.18f,10f),new Vector3(1.244f, -0.74f,10f),new Vector3(1.78f, -1.56f,10f) },
        new Vector3[] { new Vector3(1.682f, 3.72f,10f), new Vector3(1.175f, 2.95f,10f),new Vector3(0.58f, 2.05f,10f), new Vector3(-0.01f, 1.13f,10f),new Vector3(-0.61f, 0.22f,10f),new Vector3(-1.24f, -0.74f,10f),new Vector3(-1.78f, -1.56f,10f) }
    };

    public static Vector3[][] Y = new Vector3[][] {
        new Vector3[] {new Vector3(-1.54f, 3.8f,10f),new Vector3(-1.08f, 2.97f,10f),new Vector3(-0.57f, 2.04f,10f),new Vector3(0.01f, 0.98f,10f) },
        new Vector3[] {new Vector3(1.66f, 3.79f,10f),new Vector3(1.23f, 3f,10f),new Vector3(0.66f, 2.04f,10f),new Vector3(0.01f, 0.98f,10f) },
        new Vector3[] { new Vector3(0.01f, 0.98f,10f), new Vector3(0.01f, 0.19f,10f), new Vector3(0.01f, -0.67f,10f),new Vector3(0.01f, -1.61f,10f) }
    };

    public static Vector3[][] Z = new Vector3[][] {
        new Vector3[] {new Vector3(-1.43f, 3.47f, 10f),new Vector3(-0.67f, 3.47f,10f),new Vector3(0.16f, 3.47f,10f),new Vector3(1.03f, 3.47f,10f) },
        new Vector3[] { new Vector3(1.03f, 3.49f,10f) ,new Vector3(0.68f, 2.65f,10f),new Vector3(0.27f, 1.7f,10f),new Vector3(-0.15f, 0.74f,10f),new Vector3(-0.59f, -0.33f,10f),new Vector3(-1.04f, -1.39f,10f) },
        new Vector3[] { new Vector3(-1.04f, -1.39f,10f), new Vector3(-0.20f, -1.38f,10f), new Vector3 (0.64f, -1.38f,10f),new Vector3(1.47f, -1.38f,10f) }
    };
    #endregion

    public static Vector3[][][] LETTERS_POSITION_VALUES_UPPERCASE = new Vector3[][][] { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z };

    public static void DataGen(Sprite[] UPPERCASE, Sprite[] UPPERCASE_SHADOW,
									Sprite[] LOWERCASE, Sprite[] LOWERCASE_SHADOW,
                                    Sprite[] CARD_1, Sprite[] CARD_2,
                                    AudioClip[] AUDIO_FEMALE, AudioClip[] AUDIO_MALE, 
                                    AudioClip[] CARD_1_AUDIO, AudioClip[] CARD_2_AUDIO,
                                    Sprite[] TRACING_UPPERCASE_WHITE, Sprite[] TRACING_UPPERCASE_COLOR)
	{
        for (int col = 0; col < Attributes.Length; col++)
        {
            data_table.Columns.Add(Attributes[col].ToString());
        }
		data_table.Columns[1].DataType = typeof(Sprite);
		data_table.Columns[2].DataType = typeof(Sprite);
        data_table.Columns[3].DataType = typeof(Sprite);
        data_table.Columns[4].DataType = typeof(Sprite);
        data_table.Columns[5].DataType = typeof(Sprite);
        data_table.Columns[6].DataType = typeof(Sprite);
		data_table.Columns[7].DataType = typeof(AudioClip);
        data_table.Columns[8].DataType = typeof(AudioClip);
        data_table.Columns[9].DataType = typeof(AudioClip);
        data_table.Columns[10].DataType = typeof(AudioClip);
        data_table.Columns[11].DataType = typeof(Sprite);
        data_table.Columns[12].DataType = typeof(Sprite);
        data_table.Columns[13].DataType = typeof(Vector3[][]);

        int rocount = data_table.Rows.Count;
        for (int rowNo = 0; rowNo < alphabets.Length; rowNo++)
        {
            data_table.Rows.Add(data_table.NewRow());
            data_table.Rows[rocount + rowNo][0] = alphabets[rowNo].ToString();
			data_table.Rows[rocount + rowNo][1] = UPPERCASE[rowNo];			
            data_table.Rows[rocount + rowNo][2] = UPPERCASE_SHADOW[rowNo];
			data_table.Rows[rocount + rowNo][3] = LOWERCASE[rowNo];
			data_table.Rows[rocount + rowNo][4] = LOWERCASE_SHADOW[rowNo];
			data_table.Rows[rocount + rowNo][5] = CARD_1[rowNo];
			data_table.Rows[rocount + rowNo][6] = CARD_2[rowNo];
			data_table.Rows[rocount + rowNo][7] = AUDIO_FEMALE[rowNo];
            data_table.Rows[rocount + rowNo][8] = AUDIO_MALE[rowNo];
            data_table.Rows[rocount + rowNo][9] = CARD_1_AUDIO[rowNo];
            data_table.Rows[rocount + rowNo][10] = CARD_2_AUDIO[rowNo];
            data_table.Rows[rocount + rowNo][11] = TRACING_UPPERCASE_WHITE[rowNo];
            data_table.Rows[rocount + rowNo][12] = TRACING_UPPERCASE_COLOR[rowNo];
            data_table.Rows[rocount + rowNo][13] = LETTERS_POSITION_VALUES_UPPERCASE[rowNo];
        }
    }

   
    public static void CheckForNullValues(int index)
    {
        bool InvalidData = false;
        foreach (DataRow item in data_table.Select("1=1"))
        {
            if (item[index] == DBNull.Value)
            {
                Debug.Log("Null value on Index:  " + index + ": " + item[0].ToString().ToUpper());
                InvalidData = true;
            }
        }
        if (!InvalidData)
           Debug.Log("No Invalid Data Found On Index: " + index);
    }


}
