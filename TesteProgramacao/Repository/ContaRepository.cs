using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TesteProgramacao.Models;

namespace TesteProgramacao.Repository
{
    public class ContaRepository : AbstractRepository<Conta, int>
    {
        ///<summary>Exclui uma conta pela entidade
        ///<param name="entity">Referência de Conta que será excluída.</param>
        ///</summary>
        public override void Delete(Conta entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "DELETE Conta Where Id=@Id";
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

        ///<summary>Exclui uma conta pelo ID
        ///<param name="id">Id do registro que será excluído.</param>
        ///</summary>
        public override void DeleteById(int id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "DELETE Conta Where Id=@Id";
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

        ///<summary>Obtém todas as contas
        ///<returns>Retorna as contas cadastradas.</returns>
        ///</summary>
        public override List<Conta> GetAll()
        {
            string sql = "Select Id, Nome, Codigo FROM Conta ORDER BY Nome";
            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                List<Conta> list = new List<Conta>();
                Conta p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            p = new Conta
                            {
                                Id = (Guid)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                Codigo = reader["Codigo"].ToString()
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

        ///<summary>Obtém uma conta pelo ID
        ///<param name="id">Id do registro que obtido.</param>
        ///<returns>Retorna uma referência de Conta do registro encontrado ou null se ele não for encontrado.</returns>
        ///</summary>
        public override Conta GetById(int id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "Select Id, Nome, Codigo FROM Conta WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                Conta p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                p = new Conta();
                                p.Id = (Guid)reader["Id"];
                                p.Nome = reader["Nome"].ToString();
                                p.Codigo = reader["Codigo"].ToString();
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

        ///<summary>Salva a conta no banco
        ///<param name="entity">Referência de Conta que será salva.</param>
        ///</summary>
        public override void Save(Conta entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "INSERT INTO Conta (Nome, Codigo) VALUES (@Nome, @Codigo)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nome", entity.Nome);
                cmd.Parameters.AddWithValue("@Codigo", entity.Codigo);
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

        ///<summary>Atualiza a conta no banco
        ///<param name="entity">Referência de Conta que será atualizada.</param>
        ///</summary>
        public override void Update(Conta entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE Conta SET Nome=@Nome, Codigo=@Codigo Where Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Nome", entity.Nome);
                cmd.Parameters.AddWithValue("@Codigo", entity.Codigo);
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