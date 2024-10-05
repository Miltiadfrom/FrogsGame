namespace FrogsInSwampWinFormsApp
{
    public partial class MainForm : Form
    {
        private int numberMovesFrogs = 0;
        private int minNumberMovesToWins = 24;

        private System.Windows.Forms.Timer gameTimer;
        private int elapsedTime;

        public MainForm()
        {
            InitializeComponent();

            // Инициализация таймера
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000; // Установить интервал в 1 секунду
            gameTimer.Tick += GameTimer_Tick; // Подписаться на событие Tick
        }

        private void frogPictureBox_Click(object sender, EventArgs e)
        {
            Swap((PictureBox)sender);

            if (EndGame())
            {
                gameTimer.Stop(); // Остановить таймер при завершении игры

                if (CanBeFewerSteps(numberMovesLabel.Text))
                {
                    MessageBox.Show("Вы справились за минимальное кол-во ходов!");
                }
                else
                {
                    var winsMessage = MessageBox.Show("Можно улучшить результат. " +
                        "Хотите попробовать еще раз?",
                        "Конец игры", MessageBoxButtons.YesNo);
                    if (winsMessage == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                    if (winsMessage == DialogResult.No)
                    {
                        MessageBox.Show("Игра закончилась. Выберите действие в Меню.");
                    }
                }
            }
        }

        private bool CanBeFewerSteps(string numberMovesLabel)
        {
            try
            {
                var presentValueMovesFrogs = Convert.ToInt32(numberMovesLabel);

                if (presentValueMovesFrogs < minNumberMovesToWins)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        private void Swap(PictureBox clickedPicture)
        {
            var distance = Math.Abs(clickedPicture.Location.X - emptyPictureBox.Location.X) / emptyPictureBox.Size.Width;

            if (distance > 2)
            {
                MessageBox.Show("Так двигать нельзя!");
            }
            else
            {
                var location = clickedPicture.Location;

                clickedPicture.Location = emptyPictureBox.Location;

                emptyPictureBox.Location = location;

                numberMovesFrogs++;

                numberMovesLabel.Text = numberMovesFrogs.ToString();
            }
        }

        private bool EndGame()
        {
            if (frogLeftPictureBox1.Location.X > emptyPictureBox.Location.X &&
                frogLeftPictureBox2.Location.X > emptyPictureBox.Location.X &&
                frogLeftPictureBox3.Location.X > emptyPictureBox.Location.X &&
                frogLeftPictureBox4.Location.X > emptyPictureBox.Location.X &&
                frogRightPictureBox1.Location.X < emptyPictureBox.Location.X &&
                frogRightPictureBox2.Location.X < emptyPictureBox.Location.X &&
                frogRightPictureBox3.Location.X < emptyPictureBox.Location.X &&
                frogRightPictureBox4.Location.X < emptyPictureBox.Location.X)
            {
                frogLeftPictureBox1.Enabled = false;
                frogLeftPictureBox2.Enabled = false;
                frogLeftPictureBox3.Enabled = false;
                frogLeftPictureBox4.Enabled = false;

                frogRightPictureBox1.Enabled = false;
                frogRightPictureBox2.Enabled = false;
                frogRightPictureBox3.Enabled = false;
                frogRightPictureBox4.Enabled = false;

                return true;
            }

            return false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void rulesGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Короткая игра головоломка на логику.\n\n" +
                $"   На листах в болоте сидит восемь лягушек: четыре лягушки смотрят направо и четыре лягушки смотрят налево, между ними пустой листок.\n\n " +
                $"При помощи кликов мыши нужно поменять их местами, чтобы четыре лягушки которые смотрят направо оказались справа, а четыре лягушки которые смотрят налево переместились влево, " +
                $"выполнить это нужно за минимальное кол-во ходов. " +
                $"Каждая лягушка может либо переместиться вперед или назад на один шаг, либо перепрыгнуть через одну лягушку, если за ней есть свободный лист.", "Правила игры");
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            minNumberMovesToWinsLabel.Text = minNumberMovesToWins.ToString();

            // Запуск таймера при показе формы
            elapsedTime = 0; // Сброс времени
            gameTimer.Start(); // Запуск таймера
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
            UpdateElapsedTimeLabel();
        }

        private void UpdateElapsedTimeLabel()
        {
            int minutes = elapsedTime / 60;
            int seconds = elapsedTime % 60;

            elapsedTimeLabel.Text = $"Время игры: {minutes:D2}:{seconds:D2}";
        }
    }
}