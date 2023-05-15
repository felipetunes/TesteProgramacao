using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using TesteProgramacao.Models;

namespace TesteProgramacao.Repository
{
    public class CategoriaRepository : AbstractRepository<Categoria, Guid>
    {
        ///<summary>Exclui uma categoria pela entidade
        ///<param name="entity">Referência de Categoria que será excluída.</param>
        ///</summary>
        public override void Delete(Categoria entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "DELETE Categoria Where Id=@Id";
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

        ///<summary>Exclui uma categoria pelo ID
        ///<param name="id">Id do registro que será excluído.</param>
        ///</summary>
        public override void DeleteById(Guid id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "DELETE Categoria Where Id=@Id";
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

        ///<summary>Obtém todas as categorias
        ///<returns>Retorna as categorias cadastradas.</returns>
        ///</summary>
        public override List<Categoria> GetAll()
        {
            string sql = "Select Id, Nome, Tipo FROM Categoria ORDER BY Nome";
            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                List<Categoria> list = new List<Categoria>();
                Categoria p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            p = new Categoria
                            {
                                Id = (Guid)reader["Id"],
                                Nome = reader["Nome"].ToString(),
                                Tipo = Convert.ToInt32(reader["Tipo"])
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

        ///<summary>Obtém uma categoria pelo ID
        ///<param name="id">Id do registro que obtido.</param>
        ///<returns>Retorna uma referência de Categoria do registro encontrado ou null se ele não for encontrado.</returns>
        ///</summary>
        public override Categoria GetById(Guid id)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "Select Id, Nome, Tipo FROM Categoria WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                Categoria p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                p = new Categoria();
                                p.Id = (Guid)reader["Id"];
                                p.Nome = reader["Nome"].ToString();
                                p.Tipo = Convert.ToInt32(reader["Tipo"]);
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

        ///<summary>Salva a categoria no banco
        ///<param name="entity">Referência de Categoria que será salva.</param>
        ///</summary>
        public override void Save(Categoria entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "INSERT INTO Categoria (Nome, Tipo) VALUES (@Nome, @Tipo)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Nome", entity.Nome);
                cmd.Parameters.AddWithValue("@Tipo", entity.Tipo);
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

        ///<summary>Atualiza a categoria no banco
        ///<param name="entity">Referência de Categoria que será atualizada.</param>
        ///</summary>
        public override void Update(Categoria entity)
        {
            using (var conn = new SqlConnection(StringConnection))
            {
                string sql = "UPDATE Categoria SET Nome=@Nome, Tipo=@Tipo Where Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", entity.Id);
                cmd.Parameters.AddWithValue("@Nome", entity.Nome);
                cmd.Parameters.AddWithValue("@Tipo", entity.Tipo);
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