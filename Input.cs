using System.Collections;
using System.Windows.Forms;

namespace Snake
{
    internal class Input
    {
        //Lista das teclas que podem ser premidas
        private static Hashtable keyTable = new Hashtable();

        //Verifica se uma determinada tecla foi premida
        public static bool KeyPressed(Keys key)
        {
            if (keyTable[key] == null)
            {
                return false;
            }

            return (bool) keyTable[key];
        }

        //Verifica se uma tecla do teclado foi premida
        public static void ChangeState(Keys key, bool state)
        {
            keyTable[key] = state;
        }
    }
}
