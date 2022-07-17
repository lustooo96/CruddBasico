using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CruddBasico.Services
{
    class ScreenActions
    {

        public static void RefreshDataGrid(DataGridView dataGridView1 , int idCar = 0)
        {
            if (dataGridView1.Columns.Contains("Deletar")) {
                dataGridView1.Columns.Remove("Deletar");
              
            }
            if (dataGridView1.Columns.Contains("Editar")) {
                dataGridView1.Columns.Remove("Editar");
            }
  
    


            dataGridView1.DataSource = null;

            if (idCar == 0)
            {
                dataGridView1.DataSource = DataBaseOperation.SeachAllCar();
            }
            else {
                dataGridView1.DataSource = DataBaseOperation.SeachOneCar(idCar);
            }
            

            //Cria o Botão de editar no dataGrid
            DataGridViewButtonColumn EditColumn = new DataGridViewButtonColumn();
            EditColumn.HeaderText = "Editar";
            EditColumn.Text = "Editar";
            EditColumn.Name = "Editar";
            EditColumn.DataPropertyName = "Editar";
            EditColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(EditColumn);

            //Cria o Botão de deletar no dataGrid(delete por id do carro)
            DataGridViewButtonColumn DeleteColumn = new DataGridViewButtonColumn();
            DeleteColumn.HeaderText = "Deletar";
            DeleteColumn.Name = "Deletar";
            DeleteColumn.Text = "Deletar";
            DeleteColumn.DataPropertyName = "Deletar";
            DeleteColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(DeleteColumn);

        }
    }
}
