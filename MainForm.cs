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

            // ������������� �������
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000; // ���������� �������� � 1 �������
            gameTimer.Tick += GameTimer_Tick; // ����������� �� ������� Tick
        }

        private void frogPictureBox_Click(object sender, EventArgs e)
        {
            Swap((PictureBox)sender);

            if (EndGame())
            {
                gameTimer.Stop(); // ���������� ������ ��� ���������� ����

                if (CanBeFewerSteps(numberMovesLabel.Text))
                {
                    MessageBox.Show("�� ���������� �� ����������� ���-�� �����!");
                }
                else
                {
                    var winsMessage = MessageBox.Show("����� �������� ���������. " +
                        "������ ����������� ��� ���?",
                        "����� ����", MessageBoxButtons.YesNo);
                    if (winsMessage == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                    if (winsMessage == DialogResult.No)
                    {
                        MessageBox.Show("���� �����������. �������� �������� � ����.");
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
                MessageBox.Show("��� ������� ������!");
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
            MessageBox.Show($"�������� ���� ����������� �� ������.\n\n" +
                $"   �� ������ � ������ ����� ������ �������: ������ ������� ������� ������� � ������ ������� ������� ������, ����� ���� ������ ������.\n\n " +
                $"��� ������ ������ ���� ����� �������� �� �������, ����� ������ ������� ������� ������� ������� ��������� ������, � ������ ������� ������� ������� ������ ������������� �����, " +
                $"��������� ��� ����� �� ����������� ���-�� �����. " +
                $"������ ������� ����� ���� ������������� ������ ��� ����� �� ���� ���, ���� ������������ ����� ���� �������, ���� �� ��� ���� ��������� ����.", "������� ����");
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            minNumberMovesToWinsLabel.Text = minNumberMovesToWins.ToString();

            // ������ ������� ��� ������ �����
            elapsedTime = 0; // ����� �������
            gameTimer.Start(); // ������ �������
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

            elapsedTimeLabel.Text = $"����� ����: {minutes:D2}:{seconds:D2}";
        }
    }
}