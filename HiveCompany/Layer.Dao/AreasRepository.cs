using System;
using System.Collections.Generic;
using System.Configuration;
using HiveCompany.Bll;
using Npgsql;

namespace HiveCompany.Dao
{
    public class AreasRepository
    {
        private readonly string _connectionString;

        public AreasRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString; 
        }

        public bool InserirAreas(List<Area> areas)
        {            
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var area in areas)
                        {
                            using (var command = new NpgsqlCommand("SELECT public.scire_inserir_area(@nome, @uf, @cidade, @coordenadas)", connection))
                            {
                                try
                                {

                                    command.Parameters.AddWithValue("nome", area.nome);
                                    command.Parameters.AddWithValue("uf", area.uf);
                                    command.Parameters.AddWithValue("cidade", area.cidade.ToUpperInvariant());
                                    command.Parameters.AddWithValue("coordenadas", area.coordenadas);

                                    var result = (int)command.ExecuteScalar();

                                    if (result == 0)
                                    {
                                        transaction.Rollback();
                                        return false;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {                        
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public List<AreaResumo> GetAreasResumo(bool detalharCidade)
        {
            var areas = new List<AreaResumo>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT id, uf, cidade, total FROM scire_ufcidade_area(@detalharCidade);", connection))
                {
                    command.Parameters.AddWithValue("detalharCidade", detalharCidade);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var area = new AreaResumo
                            {
                                Id = reader.GetInt64(0),
                                UF = reader.GetString(1),
                                Cidade = detalharCidade ? (reader.IsDBNull(2) ? null : reader.GetString(2)) : string.Format("{0} cidade(s)" , reader.GetInt64(3)),
                                Total = reader.GetInt64(3)
                            };
                            areas.Add(area);
                        }
                    }
                }
            }

            return areas;
        }

        public List<Contidos> GetPontosContidos(string uf, string cidade)
        {
            var pontos = new List<Contidos>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("SELECT id, cep, uf, cidade, latitude, longitude FROM scire_getpontoscontidos(@puf, @pcidade);", connection))
                {
                    command.Parameters.AddWithValue("puf", uf);
                    command.Parameters.AddWithValue("pcidade", cidade);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var area = new Contidos
                            {
                                Id = reader.IsDBNull(0) ? 0 : reader.GetInt64(0),
                                Cep = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                Uf = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                Cidade = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                Latitude = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                                Longitude = reader.IsDBNull(5) ? 0 : reader.GetDouble(5)
                            };
                            pontos.Add(area);
                        }
                    }
                }
            }

            return pontos;
        }

    }
}