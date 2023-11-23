using Dapper;
using System.Text;
using TodoAPI.Component.DomainLayer.Models.Entities;
using TodoAPI.Core.DomainLayer.Data;
using TodoAPI.Core.RepositoryLayer.Repositories;

namespace TodoAPI.Component.RepositoryLayer.Repository
{
    public class UserRepository(DataContext context) : Repository<User>(context)
    {
        #region Property
        private readonly DataContext _context = context;

        #endregion

        /// <summary>
        /// Get All User
        /// </summary>
        /// <returns></returns>
        public new async Task<IEnumerable<User>> GetAll()
        {
            using var connection = this._context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM M_USER ");
            return await connection.QueryAsync<User>(sql.ToString());
        }

        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task<User> GetById(int id)
        {
            using var connection = this._context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM M_USER ");
            sql.AppendLine(" WHERE Id = @id ");
            return await connection?.QuerySingleOrDefaultAsync<User>(sql.ToString(), new { id });
        }

        /// <summary>
        /// Get User By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetByEmail(string email)
        {
            using var connection = this._context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM M_USER ");
            sql.AppendLine(" WHERE Email = @email ");
            return await connection?.QuerySingleOrDefaultAsync<User>(sql.ToString(), new { email });
        }

        /// <summary>
        /// Insert new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public new async Task Create(User user)
        {
            using var connection = this._context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("INSERT INTO M_USER ( ");
            sql.AppendLine("            UserName ");
            sql.AppendLine("          , Email ");
            sql.AppendLine("          , Role ");
            sql.AppendLine("          , PasswordHash ");
            sql.AppendLine(" ) VALUES ( ");
            sql.AppendLine("            @UserName ");
            sql.AppendLine("          , @Email ");
            sql.AppendLine("          , @Role ");
            sql.AppendLine("          , @PasswordHash ");
            sql.AppendLine("          ) ");
            await connection.ExecuteAsync(sql.ToString(), user);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public new async Task Update(User user)
        {
            using var connection = this._context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("UPDATE M_USER ");
            sql.AppendLine("   SET UserName = @UserName ");
            sql.AppendLine("     , Email = @Email ");
            sql.AppendLine("     , Role = @Role ");
            sql.AppendLine("     , PasswordHash = @PasswordHash ");
            sql.AppendLine(" WHERE Id = @Id ");
            await connection.ExecuteAsync(sql.ToString(), user);
        }

        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task Delete(int id)
        {
            using var connection = this._context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new();
            sql.AppendLine("DELETE FROM M_USER ");
            sql.AppendLine(" WHERE Id = @Id ");
            await connection.ExecuteAsync(sql.ToString(), new { id });
        }
    }
}