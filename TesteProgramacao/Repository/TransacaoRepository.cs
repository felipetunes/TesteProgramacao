using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TesteProgramacao.Models;

namespace TesteProgramacao.Repository
{
        public class TransacaoRepository : AbstractRepository<Transacao, int>
        {
            ///<summary>Exclui uma transação pela entidade
            ///<param name="entity">Referência de Transação que será excluída.</param>
            ///</summary>
            public override void Delete(Transacao entity)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "DELETE Transacao Where Id=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            ///<summary>Exclui uma transacao pelo ID
            ///<param name="id">Id do registro que será excluído.</param>
            ///</summary>
            public override void DeleteById(int id)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "DELETE Transacao Where Id=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            ///<summary>Obtém todas as transações
            ///<returns>Retorna as transações cadastradas.</returns>
            ///</summary>
            public override List<Transacao> GetAll()
            {
                string sql = "Select Id, Data, Conciliado, CAST(Notas as VARCHAR(MAX)) AS Notas FROM Transacao ORDER BY Notas";
                using (var conn = new SqlConnection(StringConnection))
                {
                    var cmd = new SqlCommand(sql, conn);
                    List<Transacao> list = new List<Transacao>();
                    Transacao p = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (reader.Read())
                            {
                                p = new Transacao
                                {
                                    Id = (Guid)reader["Id"],
                                    Notas = reader["Notas"].ToString(),
                                    Conciliado = Convert.ToInt32(reader["Conciliado"]),
                                    Data = Convert.ToDateTime(reader["Data"])
                                };
                                list.Add(p);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    return list;
                }
            }

            ///<summary>Obtém uma transação pelo ID
            ///<param name="id">Id do registro que obtido.</param>
            ///<returns>Retorna uma referência de Transação do registro encontrado ou null se ele não for encontrado.</returns>
            ///</summary>
            public override Transacao GetById(int id)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "Select Id, Data, Notas, Conciliado FROM Transacao WHERE Id=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    Transacao p = null;
                    try
                    {
                        conn.Open();
                        using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (reader.HasRows)
                            {
                                if (reader.Read())
                                {
                                    p = new Transacao();
                                    p.Id = (Guid)reader["Id"];
                                    p.Notas = reader["Notas"].ToString();
                                    p.Conciliado = Convert.ToInt32(reader["Conciliado"]);
                                    p.Data = Convert.ToDateTime(reader["Data"]);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    return p;
                }
            }

            ///<summary>Salva a transação no banco
            ///<param name="entity">Referência de Transação que será salva.</param>
            ///</summary>
            public override void Save(Transacao entity)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "INSERT INTO Transacao (Data, Notas, Conciliado) VALUES (@Data, @Notas, @Conciliado)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Notas", entity.Notas);
                    cmd.Parameters.AddWithValue("@Data", entity.Data);
                    cmd.Parameters.AddWithValue("@Conciliado", entity.Conciliado);
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            ///<summary>Atualiza a transação no banco
            ///<param name="entity">Referência de Transação que será atualizada.</param>
            ///</summary>
            public override void Update(Transacao entity)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "UPDATE Transacao SET Notas=@Notas, Codigo=@Codigo, Conciliado=@Conciliado Where Id=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@Notas", entity.Notas);
                    cmd.Parameters.AddWithValue("@Data", entity.Data);
                    cmd.Parameters.AddWithValue("@Conciliado", entity.Conciliado);
                try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }
}