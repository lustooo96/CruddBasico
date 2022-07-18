using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CruddBasico.Services
{
    class ValidationForm
    {

        public static bool ValidaFormulario(Carro car) {
            
            

            if (!ValidaModelo(car.Modelo))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("O registro não foi cadastrado , pois o modelo está invalido!", "Falha no Cadastro", System.Windows.MessageBoxButton.OK);
                return false;
            }
            else if (!ValidaPlaca(car.Placa)) 
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("O registro não foi cadastrado , pois a placa está invalida!", "Falha no Cadastro", System.Windows.MessageBoxButton.OK);
                return false;
            }
            else if (!ValidaChassi(car.Chassi))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("O registro não foi cadastrado , pois o chassi está invalido!", "Falha no Cadastro", System.Windows.MessageBoxButton.OK);
                return false;
            }
            else if (!ValidaAno(car.Ano.ToString()))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("O registro não foi cadastrado , pois o ano está invalido!", "Falha no Cadastro", System.Windows.MessageBoxButton.OK);
                return false;
            }
            else if (!ValidaMarca(car.Marca))
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("O registro não foi cadastrado, pois a marca está invalida!", "Falha no Cadastro", System.Windows.MessageBoxButton.OK);
                return false;
            }

            return true;
        }
        private static bool ValidaModelo(string modelo) {
            var checkModelo = modelo.Replace("-", "").Trim();
            var placaPadraoRegex = new Regex("[a-zA-Z]");
            return placaPadraoRegex.IsMatch(checkModelo);
        }

        private static bool ValidaPlaca(string placa)
        {
            var checkPlaca = placa.Replace("-", "").Trim();
            var placaPadraoRegex = new Regex("[a-zA-Z]{3}[0-9]{4}");
            return placaPadraoRegex.IsMatch(placa);

        }

        private static bool ValidaChassi(string chassi)
        {
            var checkChassi = chassi.Replace("-", "").Trim();
            var placaPadraoRegex = new Regex("[a-zA-Z][0-9]");
            return placaPadraoRegex.IsMatch(checkChassi);
        }

        private static bool ValidaAno(string ano)
        {

            var day = DateTime.Now.ToString("yyyy");
            var dayNumber = Convert.ToInt32(day);
            var dayForm = Convert.ToInt32(ano);
            if (1950 < dayForm && dayForm <= dayNumber + 1) {
                var checkAno = ano.Replace("-", "").Trim();
                var placaPadraoRegex = new Regex("[0-9]");
                return placaPadraoRegex.IsMatch(checkAno);
            }else
            {
                return false;
            }
            
        }
        private static bool ValidaMarca(string marca)
        {
            var checkMarca = marca.Replace("-", "").Trim();
            var placaPadraoRegex = new Regex("[a-zA-Z]");
            return placaPadraoRegex.IsMatch(checkMarca);
        }
    }
}
