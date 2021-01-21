    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    namespace Snake
    {
        public partial class Form1 : Form
        {
            private List<Circle> Snake = new List<Circle>();
            private Circle food = new Circle();

            public Form1()
            {
                InitializeComponent();

                //Define as definições padrão do jogo
                new Settings();

                //Velocidade do jogo e timer
                gameTimer.Interval = 1000 / Settings.Speed;
                gameTimer.Tick += UpdateScreen;
                gameTimer.Start();

                //Invoca o método para começar um novo jogo
                StartGame();
            }

            private void StartGame()
            {
                lblGameOver.Visible = false;

                //Set settings to default
                new Settings();

                //Create new player object
                Snake.Clear();
                Circle head = new Circle {X = 10, Y = 5};
                Snake.Add(head);


                lblScore.Text = Settings.Score.ToString();
                GenerateFood();

            }

            //Método para criar a comida e coloca-la num local aleatório do mapa
            private void GenerateFood()
            {
                int maxXPos = pbCanvas.Size.Width / Settings.Width;
                int maxYPos = pbCanvas.Size.Height / Settings.Height;

                Random random = new Random();
                food = new Circle {X = random.Next(0, maxXPos), Y = random.Next(0, maxYPos)};
            }

            //Método para atualizar o ecrã quando o jogo acaba
            private void UpdateScreen(object sender, EventArgs e)
            {
                //Quando o jogo acaba
                if (Settings.GameOver)
                {
                    //Se o Enter for premido após o jogo acabar o jogo recomeça
                    if (Input.KeyPressed(Keys.Enter))
                    {
                        StartGame();
                    }
                }
                else
                {
                    if (Input.KeyPressed(Keys.Right) && Settings.direction != Direction.Left)
                        Settings.direction = Direction.Right;
                    else if (Input.KeyPressed(Keys.Left) && Settings.direction != Direction.Right)
                        Settings.direction = Direction.Left;
                    else if (Input.KeyPressed(Keys.Up) && Settings.direction != Direction.Down)
                        Settings.direction = Direction.Up;
                    else if (Input.KeyPressed(Keys.Down) && Settings.direction != Direction.Up)
                        Settings.direction = Direction.Down;

                    MovePlayer();
                }

                pbCanvas.Invalidate();

            }
            
            //Método para criar a cobra
            private void pbCanvas_Paint(object sender, PaintEventArgs e)
            {
                Graphics canvas = e.Graphics;

                if (!Settings.GameOver)
                {
                    //Criar a cobra
                    for (int i = 0; i < Snake.Count; i++)
                    {
                        Brush snakeColour;
                        if (i == 0)
                            snakeColour = Brushes.Black;     //Desenhar a cabeça
                        else
                            snakeColour = Brushes.DarkGray;    //Desenhar o resto do corpo

                        //Desenhar a cobra
                        canvas.FillEllipse(snakeColour,
                            new Rectangle(Snake[i].X * Settings.Width,
                                          Snake[i].Y * Settings.Height,
                                          Settings.Width, Settings.Height));


                        //Desenhar a comida
                        canvas.FillEllipse(Brushes.Green,
                            new Rectangle(food.X * Settings.Width,
                                 food.Y * Settings.Height, Settings.Width, Settings.Height));

                    }
                }
                else
                {
                    string gameOver = "Game over \nYour final score is: " + Settings.Score + "\nPress Enter to try again";
                    lblGameOver.Text = gameOver;
                    lblGameOver.Visible = true;
                }
            }


            private void MovePlayer()
            {
                for (int i = Snake.Count - 1; i >= 0; i--)
                {
                    //Mover a cabeça
                    if (i == 0)
                    {
                        switch (Settings.direction)
                        {
                            case Direction.Right:
                                Snake[i].X++;
                                break;
                            case Direction.Left:
                                Snake[i].X--;
                                break;
                            case Direction.Up:
                                Snake[i].Y--;
                                break;
                            case Direction.Down:
                                Snake[i].Y++;
                                break;
                        }


                        int maxXPos = pbCanvas.Size.Width / Settings.Width;
                        int maxYPos = pbCanvas.Size.Height / Settings.Height;

                        //Detetar o toque com as bordas do mapa
                        if (Snake[i].X < 0 || Snake[i].Y < 0
                            || Snake[i].X >= maxXPos || Snake[i].Y >= maxYPos)
                        {
                            Die();
                        }


                        //Detetar o toque com o próprio corpo
                        for (int j = 1; j < Snake.Count; j++)
                        {
                            if (Snake[i].X == Snake[j].X &&
                               Snake[i].Y == Snake[j].Y)
                            {
                                Die();
                            }
                        }

                        //Detetar o toque com a comida
                        if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                        {
                            Eat();
                        }

                    }
                    else
                    {
                        //Mover o corpo
                        Snake[i].X = Snake[i - 1].X;
                        Snake[i].Y = Snake[i - 1].Y;
                    }
                }
            }

            private void Form1_KeyDown(object sender, KeyEventArgs e)
            {
                Input.ChangeState(e.KeyCode, true);
            }

            private void Form1_KeyUp(object sender, KeyEventArgs e)
            {
                Input.ChangeState(e.KeyCode, false);
            }
            
            //Método para a comida
            private void Eat()
            {
                //Ao comer, adicionar um circulo ao corpo da cobra
                Circle circle = new Circle
                {
                    X = Snake[Snake.Count - 1].X,
                    Y = Snake[Snake.Count - 1].Y
                };
                Snake.Add(circle);

                //Atualizar Score
                Settings.Score += Settings.Points;
                lblScore.Text = Settings.Score.ToString();

                GenerateFood();
            }
            
            //Metodo para a cobra morrer
            private void Die()
            {
                Settings.GameOver = true;
            }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    }
