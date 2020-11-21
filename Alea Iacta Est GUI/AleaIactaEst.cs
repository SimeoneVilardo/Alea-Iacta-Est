using System;
using System.Windows.Forms;

namespace Alea_Iacta_Est_GUI
{
    public partial class AleaIactaEst : Form
    {
        int coins = 50;
        Random rnd = new Random();

        public AleaIactaEst()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            int bet, guess;
            string strBet = txtBet.Text;
            bool isValidBet = int.TryParse(strBet, out bet) && bet > 0 && bet <= coins;
            if (!isValidBet)
            {
                MessageBox.Show(string.Format("Devi scommettere un numero di sesterzi compreso tra 1 e {0}", coins), "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string strGuess = txtGuess.Text;
            bool isValidGuess = int.TryParse(strGuess, out guess) && guess > 1 && guess < 13;
            if (!isValidGuess)
            {
                MessageBox.Show("La somma dei dadi scommessa deve essere compresa tra 2 e 12", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            coins = coins - bet;
            lblCoins.Text = string.Format("Hai {0} sesterzi", coins);
            int dice1 = rnd.Next(1, 7);
            int dice2 = rnd.Next(1, 7);
            int sum = dice1 + dice2;
            lblDice1.Text = string.Format("Primo dado: {0}", dice1);
            lblDice2.Text = string.Format("Secondo dado: {0}", dice2);
            lblDiceSum.Text = string.Format("La somma dei due dadi lanciati è \"{0}\" e tu avevi scomesso su \"{1}\"", sum, guess);
            if (sum == guess)
            {
                int reward = bet * 10;
                coins = coins + reward;
                lblCoins.Text = string.Format("Hai {0} sesterzi", coins);
                lblResult.Text = string.Format("Indovinato! Hai vinto {0} sesterzi! Ora hai in totale {1} sesterzi", reward, coins);
            }
            else
            {
                lblResult.Text = "Hai perso...";
            }
            if (coins <= 0)
            {
                btnPlay.Enabled = false;
                txtBet.Enabled = false;
                txtGuess.Enabled = false;
                MessageBox.Show("Hai terminato tutti i sesterzi a tua disposizione, non puoi più giocare", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            txtBet.Clear();
            txtGuess.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sei sicuro di voler uscire?", "Esci", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
                Application.Exit();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            int coins = 50;
            btnPlay.Enabled = true;
            txtBet.Enabled = true;
            txtGuess.Enabled = true;
            lblCoins.Text = string.Format("Hai {0} sesterzi", coins);
            lblDiceSum.Text = "I dadi non sono stati ancora lanciati";
            lblDice1.Text = "Primo dado:";
            lblDice2.Text = "Secondo dado:";
            lblResult.Text = string.Empty;
            txtBet.Clear();
            txtGuess.Clear();
        }
    }
}