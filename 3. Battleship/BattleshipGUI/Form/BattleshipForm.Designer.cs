using BattleshipGUI.Models;
using BattleshipGUI.Settings;
using System.Windows.Forms;

namespace BattleshipGUI {
    partial class BattleshipForm {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = FormSettings.FormSize;
            this.Text = FormSettings.FormTitle;
        }

        #endregion

        private void InitializeForm() {
            InitializeComponent();
            InitializeStartStopButtons();
            InitializePanels();
            InitializePlayerButtons();
            InitializePlayerLabels();
            InitializeMessageLabel();
        }
        private void InitializeStartStopButtons() {
            InitializeStartButton();
            InitializeStopButton();
        }
        private void InitializeStartButton() {
            StartButton = new Button();
            StartButton.Size = FormSettings.StartStopButtonSize;
            StartButton.Location = FormSettings.StartStopButtonCoords;
            StartButton.Text = FormSettings.StartButtonText;
            StartButton.Font = FormSettings.StartStopButtonFont;
            StartButton.Font = new Font(StartButton.Font, FontStyle.Bold);
            StartButton.Click += StartButtonClick!;
            this.Controls.Add(StartButton);
        }
        private void StartButtonClick(Object sender, EventArgs args) {
            StartButton.Visible = false;
            StartButton.Enabled = false;
            StopButton.Visible = true;
            StopButton.Enabled = true;
        }
        private void InitializeStopButton() {
            StopButton = new Button();
            StopButton.Size = FormSettings.StartStopButtonSize;
            StopButton.Location = FormSettings.StartStopButtonCoords;
            StopButton.Text = FormSettings.StopButtonText;
            StopButton.Font = FormSettings.StartStopButtonFont;
            StopButton.Click += StopButtonClick!;
            this.Controls.Add(StopButton);
        }
        private void StopButtonClick(Object sender, EventArgs args) {
            StopButton.Visible = false;
            StopButton.Enabled = false;
            StartButton.Visible = true;
            StartButton.Enabled = true;
        }
        private void InitializePanels() {
            InitializePlayerPanels();
            InitializeNumberPanels();
            InitializeLetterPanels();
        }
        private void InitializePlayerPanels() {
            InitializeUserPanel();
            InitializeAIPanel();
        }
        private void InitializeUserPanel() {
            UserPanel = new FlowLayoutPanel();
            UserPanel.Size = FormSettings.PlayerPanelSize;
            UserPanel.Location = FormSettings.UserPanelCoords;
            UserPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(UserPanel);
        }
        private void InitializeAIPanel() {
            AIPanel = new FlowLayoutPanel();
            AIPanel.Size = FormSettings.PlayerPanelSize;
            AIPanel.Location = FormSettings.AIPanelCoords;
            AIPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(AIPanel);
        }
        private void InitializeNumberPanels() {
            InitializeUserNumberPanel();
            InitializeAINumberPanel();
        }
        private void InitializeUserNumberPanel() {
            var userNumberPanel = new FlowLayoutPanel();
            userNumberPanel.Size = FormSettings.NumberPanelSize;
            userNumberPanel.Location = FormSettings.UserNumberPanelCoords;
            userNumberPanel.BorderStyle = BorderStyle.FixedSingle;
            for (int i = 1; i <= 10; i++) {
                var userNumberLabel = new Label();
                userNumberLabel.Size = FormSettings.NumberLabelSize;
                userNumberLabel.Font = FormSettings.NumberLabelFont;
                userNumberLabel.Margin = FormSettings.NumberLabelMargin;
                userNumberLabel.TextAlign = ContentAlignment.MiddleCenter;
                userNumberLabel.Text = i.ToString();
                userNumberPanel.Controls.Add(userNumberLabel);
            }
            this.Controls.Add(userNumberPanel);
        }
        private void InitializeAINumberPanel() {
            var aiNumberPanel = new FlowLayoutPanel();
            aiNumberPanel.Size = FormSettings.NumberPanelSize;
            aiNumberPanel.Location = FormSettings.AINumberPanelCoords;
            aiNumberPanel.BorderStyle = BorderStyle.FixedSingle;
            for (int i = 1; i <= 10; i++) {
                var aiNumberLabel = new Label();
                aiNumberLabel.Size = FormSettings.NumberLabelSize;
                aiNumberLabel.Font = FormSettings.NumberLabelFont;
                aiNumberLabel.Margin = FormSettings.NumberLabelMargin;
                aiNumberLabel.TextAlign = ContentAlignment.MiddleCenter;
                aiNumberLabel.Text = i.ToString();
                aiNumberPanel.Controls.Add(aiNumberLabel);
            }
            this.Controls.Add(aiNumberPanel);
        }
        private void InitializeLetterPanels() {
            InitializeUserLetterPanel();
            InitializeAILetterPanel();
        }
        private void InitializeUserLetterPanel() {
            var userLetterPanel = new FlowLayoutPanel();
            userLetterPanel.Size = FormSettings.LetterPanelSize;
            userLetterPanel.Location = FormSettings.UserLetterPanelCoords;
            userLetterPanel.BorderStyle = BorderStyle.FixedSingle;
            for (char c = 'A'; c <= 'J'; c++) {
                var userLetterLabel = new Label();
                userLetterLabel.Size = FormSettings.LetterLabelSize;
                userLetterLabel.Font = FormSettings.LetterLabelFont;
                userLetterLabel.Margin = FormSettings.LetterLabelMargin;
                userLetterLabel.TextAlign = ContentAlignment.MiddleCenter;
                userLetterLabel.Text = c.ToString();
                userLetterPanel.Controls.Add(userLetterLabel);
            }
            this.Controls.Add(userLetterPanel);
        }
        private void InitializeAILetterPanel() {
            var aiLetterPanel = new FlowLayoutPanel();
            aiLetterPanel.Size = FormSettings.LetterPanelSize;
            aiLetterPanel.Location = FormSettings.AILetterPanelCoords;
            aiLetterPanel.BorderStyle = BorderStyle.FixedSingle;
            for (char c = 'A'; c <= 'J'; c++) {
                var aiLetterLabel = new Label();
                aiLetterLabel.Size = FormSettings.LetterLabelSize;
                aiLetterLabel.Font = FormSettings.LetterLabelFont;
                aiLetterLabel.Margin = FormSettings.LetterLabelMargin;
                aiLetterLabel.TextAlign = ContentAlignment.MiddleCenter;
                aiLetterLabel.Text = c.ToString();
                aiLetterPanel.Controls.Add(aiLetterLabel);
            }
            this.Controls.Add(aiLetterPanel);
        }
        private void InitializePlayerButtons() {
            InitializeUserButtons();
            InitializeAIButtons();
        }
        private void InitializeUserButtons() {
            UserButtons = new Button[10, 10];
            for (int row = 0; row < 10; row++)
                for (int column = 0; column < 10; column++) {
                    UserButtons[row, column] = new Button();
                    UserButtons[row, column].Size = FormSettings.FieldButtonSize;
                    UserButtons[row, column].Text = FormSettings.EmptyFieldButtonMark;
                    UserButtons[row, column].Font = FormSettings.FieldButtonFont;
                    UserButtons[row, column].DataContext = new Cell(row, column);
                    UserPanel.Controls.Add(UserButtons[row, column]);
                }
        }
        private void InitializeAIButtons() {
            AIButtons = new Button[10, 10];
            for (int row = 0; row < 10; row++)
                for (int column = 0; column < 10; column++) {
                    AIButtons[row, column] = new Button();
                    AIButtons[row, column].Size = FormSettings.FieldButtonSize;
                    AIButtons[row, column].Text = FormSettings.EmptyFieldButtonMark;
                    AIButtons[row, column].Font = FormSettings.FieldButtonFont;
                    AIButtons[row, column].DataContext = new Cell(row, column);
                    AIPanel.Controls.Add(AIButtons[row, column]);
                }
        }
        private void InitializePlayerLabels() {
            InitializeUserPlayerLabel();
            InitializeAIPlayerLabel();
        }
        private void InitializeUserPlayerLabel() {
            var userLabel = new Label();
            userLabel.Size = FormSettings.PlayerLabelSize;
            userLabel.Location = FormSettings.UserPlayerLabelCoords;
            userLabel.Text = FormSettings.UserPlayerLabelText;
            userLabel.Font = FormSettings.PlayerLabelFont;
            userLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(userLabel);
        }
        private void InitializeAIPlayerLabel() {
            var aiLabel = new Label();
            aiLabel.Size = FormSettings.PlayerLabelSize;
            aiLabel.Location = FormSettings.AIPlayerLabelCoords;
            aiLabel.Text = FormSettings.AIPlayerLabelText;
            aiLabel.Font = FormSettings.PlayerLabelFont;
            aiLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(aiLabel);
        }
        private void InitializeMessageLabel() {
            MessageLabel = new Label();
            MessageLabel.Size = FormSettings.MessageLabelSize;
            MessageLabel.Location = FormSettings.MessageLabelCoords;
            MessageLabel.Text = FormSettings.MessageLabelText;
            MessageLabel.Font = FormSettings.MessageLabelFont;
            MessageLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(MessageLabel);
        }
    }
}