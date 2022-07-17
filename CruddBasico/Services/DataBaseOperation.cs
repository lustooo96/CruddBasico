using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CruddBasico.Services
{
    class DataBaseOperation
    {

        public static void SaveCar(Carro car) {
            try {
                using (var transaction = new TransactionScope())
                {
                    using (var db = new BancoTesteDB())
                    {
                        db.Carro
                            .Value(p => p.Modelo, car.Modelo)
                            .Value(p => p.Placa, car.Placa)
                            .Value(p => p.Chassi, car.Chassi)
                            .Value(p => p.Ano, car.Ano)
                            .Value(p => p.Marca, car.Marca)
                            .Insert();
                    }
                    transaction.Complete();
                }
            }
            catch (InvalidCastException e)
            {
                // recover from exception
            }    
        }

        public static void DeleteCar(int idCar)
        {
            try 
            {
                using (var transaction = new TransactionScope())
                {
                    using (var db = new BancoTesteDB())
                    {
                        db.Carro
                            .Where(p => p.Idcarro == idCar)
                            .Delete();
                    }
                    transaction.Complete();
                }
            }
            catch (InvalidCastException e)
            {
                // recover from exception
            }
            
        }

        public static void UpdateCar(Carro car,int idCar)
        {
            try {

                //Eu uso transation com mais de um 
                using (var transaction = new TransactionScope())
                {
                    using (var db = new BancoTesteDB())
                    {
                        db.Carro
                          .Where(p => p.Idcarro == idCar)
                          .Set(p => p.Modelo, car.Modelo)
                          .Set(p => p.Placa, car.Placa)
                          .Set(p => p.Chassi, car.Chassi)
                          .Set(p => p.Ano, car.Ano)
                          .Set(p => p.Marca, car.Marca)
                          .Update();
                    }
                    transaction.Complete();
                }
            }
            catch (InvalidCastException e)
            {
                // recover from exception
            }
            
        }
        public static List<Carro> SeachAllCar() {
            using (var db = new BancoTesteDB())
            {
                var query =
                    from carro in db.Carro
                    select carro;

                return query.ToList();
            }
        }
        public static List<Carro> SeachOneCar(int idCar)
        {
            using (var db = new BancoTesteDB())
            {
                var query =
                    from carro in db.Carro
                    where carro.Idcarro == idCar
                    select carro;

                return query.ToList();
            }
        }
    }
}
