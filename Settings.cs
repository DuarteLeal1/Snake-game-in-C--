namespace Snake
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };

    public class Settings
    {
        //Conjunto de métodos que cria o eixo do x, o eixo do y, a velocidade, o score, os pontos e o fim do jogo
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static int Points { get; set; }
        public static bool GameOver { get; set; }
        public static Direction direction { get; set; }
        public Settings()
        {
            Width = 16;
            Height = 16;
            Speed = 12;
            Score = 0;
            Points = 100;
            GameOver = false;
            direction = Direction.Down;
        }
    }


}
