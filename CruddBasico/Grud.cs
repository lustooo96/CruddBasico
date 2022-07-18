using CruddBasico.Services;
using DataModels;
using LinqToDB;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Drawing;

namespace CruddBasico
{
    public partial class Grud : Form
    {
        private bool edite = false;
        public Grud()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Buscar todos os dados e exibir na tela 
            ScreenActions.RefreshDataGrid(dataGridView1);

        }

        //ação de click do botão de criar um novo registro e salvar um registro já existente 
        private void save_Click(object sender, EventArgs e)
        {
            //Instancia um novo carro para receber as informações do formulario
            var carro = new Carro();
            carro.Modelo = modelo.Text;
            carro.Placa = placa.Text;
            carro.Chassi = chassi.Text;

            if (ano.Text != "" ) {
                carro.Ano = Convert.ToInt32(ano.Text);
            }
           carro.Marca = marca.Text;

            //Valida se é pra editar ou criar um novo registro

            var valid = ValidationForm.ValidaFormulario(carro);
            if (valid) { 

                if (!edite)
                {
                    //Faz um create no banco com os dados do carro
                    DataBaseOperation.SaveCar(carro);
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("O registro foi cadastrado com sucesso!", "Registro Cadastrado", System.Windows.MessageBoxButton.OK);

                }
                if (edite)
                {
                    //Faz um update no banco com os dados do carro por IdCar(Identificador do carro)
                    DataBaseOperation.UpdateCar(carro, Convert.ToInt32(id.Text));

                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("O registro foi alterado com sucesso!", "O registro foi alterado", System.Windows.MessageBoxButton.OK);
                    edite = false;
                    cancel.Enabled = false;
                    label_id.Visible = false;
                    id.Visible = false;

                }

                //Busca no banco as atualição e novos registros para atualizar a gridView
                ScreenActions.RefreshDataGrid(dataGridView1);
                clearForm();

            }

        }


        //Serve para cancelar o update de um registro especifico
        private void cancel_Click(object sender, EventArgs e)
        {
            edite = false;
            id.Visible = false;
            label_id.Visible = false;
            cancel.Enabled = false;

            clearForm();
        }

        //Serve para Excluir e Editar um registro 
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //e.ColumnIndex == 6(Recebe o botão de Editar o registro da grid selecionada
            if (e.ColumnIndex == 6)
            {

                modelo.Text = dataGridView1.CurrentRow.Cells["Modelo"].Value.ToString();
                placa.Text = dataGridView1.CurrentRow.Cells["Placa"].Value.ToString();
                chassi.Text = dataGridView1.CurrentRow.Cells["Chassi"].Value.ToString();
                ano.Text = dataGridView1.CurrentRow.Cells["Ano"].Value.ToString();
                marca.Text = dataGridView1.CurrentRow.Cells["Marca"].Value.ToString();
                id.Text = dataGridView1.CurrentRow.Cells["IdCarro"].Value.ToString();
                edite = true;
                cancel.Enabled = true;
                label_id.Visible = true;
                id.Visible = true;

            }
            //e.ColumnIndex == 7 (Recebe o botão de Deletar o registro da grid selecionada)
            if (e.ColumnIndex == 7)
            {

                var idCar = dataGridView1.CurrentRow.Cells["IdCarro"].Value.ToString();
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Você quer deletar esse registro?", "Confirmação De Deletar", System.Windows.MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    DataBaseOperation.DeleteCar(Convert.ToInt32(idCar));
                    ScreenActions.RefreshDataGrid(dataGridView1);

                }


            }
        }

        private void clearForm() {
            modelo.Text = "";
            placa.Text = "";
            chassi.Text = "";
            ano.Text = "";
            marca.Text = "";
            id.Text = "";
        }

        private void textSearchById_TextChanged(object sender, EventArgs e)
        {
            //Faz a filtração da busca 
            Regex isnumber = new Regex("[^0-9]");
            if (textBox6.Text != "" && !isnumber.IsMatch(textBox6.Text))
            {
                ScreenActions.RefreshDataGrid(dataGridView1, Convert.ToInt32(textBox6.Text));
            }
            else if (textBox6.Text == "")
            {
                ScreenActions.RefreshDataGrid(dataGridView1);
            }
        }

        private void ano_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textSearchById_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "Informe o id do carro";
            }
            textBox6.ForeColor = Color.DarkGray;
        }

        private void textSearchById_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Informe o id do carro")
            {
                textBox6.Text = null;
            }
            textBox6.ForeColor = Color.Black;

        }

        private void textSearchById_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        
        }
    }
}
