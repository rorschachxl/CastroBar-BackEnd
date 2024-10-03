using CASTROBAR_API.Dtos;
using System.Data;
using System.Data.SqlClient;

namespace CASTROBAR_API.Repositories
{
    public class UsuarioRepositorio
    {
        private readonly string _connectionString;

        public UsuarioRepositorio(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<UsuarioRequestDto> RegistroUsuario(UsuarioRequestDto usuario)
        {
            string insertQuery = @"INSERT INTO USUARIO 
                (numeroDocumento, nombre, apellido, correo, telefono, DOCUMENTO_idDocumento, ESTADO_idEstado, contraseña, ROL_idRol) 
                VALUES (@numeroDocumento, @nombre, @apellido, @correo, @telefono, @DOCUMENTO_idDocumento, @ESTADO_idEstado, @contraseña, @ROL_idRol)";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@numeroDocumento", usuario.numeroDocumento);
                    cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                    cmd.Parameters.AddWithValue("@apellido", usuario.apellido);
                    cmd.Parameters.AddWithValue("@correo", usuario.correo);
                    cmd.Parameters.AddWithValue("@telefono", usuario.telefono);
                    cmd.Parameters.AddWithValue("@DOCUMENTO_idDocumento", usuario.idDocumento);
                    cmd.Parameters.AddWithValue("@ESTADO_idEstado", usuario.idEstado);
                    cmd.Parameters.AddWithValue("@contraseña", usuario.contraseña);
                    cmd.Parameters.AddWithValue("@ROL_idRol", usuario.idRol);

                    await cmd.ExecuteNonQueryAsync();
                }
            }

            return usuario;
        }

        public async Task<bool> BuscarPersona(string documento)
        {
            string query = @"SELECT COUNT (*) FROM USUARIO WHERE numeroDocumento = @numeroDocumento ";
            int encontrados = 0;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@numeroDocumento", documento);
                    encontrados = (int)cmd.ExecuteScalar();
                }
                await con.CloseAsync();

                if (encontrados > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<UsuarioResponseDto> ObtenerUsuarioPorDocumento(string documento)
        {
            UsuarioResponseDto usuarioResponseDto = null;
            string query = @"SELECT nombre, apellido, telefono, correo FROM USUARIO WHERE numeroDocumento = @numeroDocumento";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@numeroDocumento", documento);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (await reader.ReadAsync())
                        {
                            usuarioResponseDto = new UsuarioResponseDto
                            {
                                numeroDocumento = documento,
                                nombre = (string)reader["nombre"],
                                apellido = (string)reader["apellido"],
                                correo = (string)reader["correo"],
                                telefono = (string)reader["telefono"]
                            };

                        }
                    }
                }
                await con.CloseAsync();
            }
            return usuarioResponseDto;
        }

        public async Task<int> ActualizarUsuario(string numeroDocumento, string nombre, string apellido, string correo, string telefono)
        {
            int actualizado = 0;
            string query = @"UPDATE USUARIO SET nombre=@nombre, apellido=@apellido, correo=@correo, telefono=@telefono WHERE numeroDocumento=@numeroDocumento";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    await con.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellido", apellido);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@numeroDocumento", numeroDocumento);

                        actualizado = await cmd.ExecuteNonQueryAsync();
                    }
                    await con.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar " + ex.ToString());
            }
            return actualizado;
        }
    }
}
