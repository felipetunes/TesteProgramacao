using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TesteProgramacao.Models;

namespace TesteProgramacao.Repository
{
        public class TransacaoRepository : AbstractRepository<Transacao, Guid>
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
            public override void DeleteById(Guid id)
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
                string sql = "Select Id, ContaID, CategoriaID, Data, Conciliado, Historico, CAST(Notas as VARCHAR(MAX)) AS Notas, CAST(Debito AS decimal(34,4)) AS Debito, CAST(Credito AS decimal(34,4)) AS Credito FROM Transacao ORDER BY Data DESC";
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
                                    Historico = reader["Historico"].ToString(),
                                    Credito = Convert.ToDecimal(reader["Credito"]),
                                    Debito = Convert.ToDecimal(reader["Debito"]),
                                    ContaID = (Guid)reader["ContaID"],
                                    CategoriaID = (Guid)reader["CategoriaID"],
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

        ///<summary>Obtém todas as transações
        ///<returns>Retorna as transações cadastradas.</returns>
        ///</summary>
        public List<Transacao> GetTransacaoByContaId(Guid contaId)
        {
            string sql = "Select Id, ContaID, CategoriaID, Data, Notas, Credito, Debito, Conciliado, Historico FROM Transacao WHERE contaId=@contaId";
            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@contaId", contaId);
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
                                Historico = reader["Historico"].ToString(),
                                Credito = Convert.ToDecimal(reader["Credito"]),
                                Debito = Convert.ToDecimal(reader["Debito"]),
                                ContaID = (Guid)reader["ContaID"],
                                CategoriaID = (Guid)reader["CategoriaID"],
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
        public override Transacao GetById(Guid id)
            {
                using (var conn = new SqlConnection(StringConnection))
                {
                    string sql = "Select Id, ContaID, CategoriaID, Data, Notas, Credito, Debito, Conciliado, Historico FROM Transacao WHERE Id=@Id";
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
                                    p.Historico = reader["Historico"].ToString();
                                    p.Debito = Convert.ToDecimal(reader["Debito"]);
                                    p.Credito = Convert.ToDecimal(reader["Credito"]);
                                    p.ContaID = (Guid)reader["ContaID"];
                                    p.CategoriaID = (Guid)reader["CategoriaID"];
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
                    string sql = "INSERT INTO Transacao (Data, ContaID, CategoriaID, Debito, Credito, Notas, Conciliado, Historico) VALUES (@Data, @ContaID, @CategoriaID, @Debito, @Credito, @Notas, @Conciliado, @Historico)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Notas", entity.Notas);
                    cmd.Parameters.AddWithValue("@Data", entity.Data);
                    cmd.Parameters.AddWithValue("@Conciliado", entity.Conciliado);
                    cmd.Parameters.AddWithValue("@Historico", entity.Historico);
                    cmd.Parameters.AddWithValue("@Debito", entity.Debito);
                    cmd.Parameters.AddWithValue("@Credito", entity.Credito);
                    cmd.Parameters.AddWithValue("@ContaID", entity.ContaID);
                    cmd.Parameters.AddWithValue("@CategoriaID", entity.CategoriaID);
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
                    string sql = "UPDATE Transacao SET Notas=@Notas, ContaID=@ContaID, CategoriaID=@CategoriaID, Data=@Data, Conciliado=@Conciliado, Historico=@Historico, Debito=@Debito, Credito=@Credito Where Id=@Id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", entity.Id);
                    cmd.Parameters.AddWithValue("@Notas", entity.Notas);
                    cmd.Parameters.AddWithValue("@Data", entity.Data);
                    cmd.Parameters.AddWithValue("@Conciliado", entity.Conciliado);
                    cmd.Parameters.AddWithValue("@Historico", entity.Historico);
                    cmd.Parameters.AddWithValue("@Debito", entity.Debito);
                    cmd.Parameters.AddWithValue("@Credito", entity.Credito);
                    cmd.Parameters.AddWithValue("@ContaID", entity.Historico);
                    cmd.Parameters.AddWithValue("@CategoriaID", entity.Historico);
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