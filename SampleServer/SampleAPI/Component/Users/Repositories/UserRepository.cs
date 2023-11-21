using Dapper;
using System.Text;
using SampleAPI.Core.DomainLayer.Data;
using SampleAPI.Core.RepositoryLayer.Repositories;
using SampleAPI.Component.Users.Models.Entities;

namespace SampleAPI.Component.RepositoryLayer.Repository
{
    public class UserRepository : Repository<User>
    {
        #region Property
        private readonly DataContext _context;
        #endregion

        /// <summary>
        /// Constructor get instance of context
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(DataContext context) : base(context)
        {
            this._context = context;
        }

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
            sql.AppendLine("  FROM Users ");
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
            sql.AppendLine("  FROM Users ");
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
            sql.AppendLine("  FROM Users ");
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
            sql.AppendLine("INSERT INTO Users ( ");
            sql.AppendLine("            Title ");
            sql.AppendLine("          , FirstName ");
            sql.AppendLine("          , LastName ");
            sql.AppendLine("          , Email ");
            sql.AppendLine("          , Role ");
            sql.AppendLine("          , PasswordHash ");
            sql.AppendLine(" ) VALUES ( ");
            sql.AppendLine("            @Title ");
            sql.AppendLine("          , @FirstName ");
            sql.AppendLine("          , @LastName ");
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
            sql.AppendLine("UPDATE Users ");
            sql.AppendLine("   SET Title = @Title ");
            sql.AppendLine("     , FirstName = @FirstName ");
            sql.AppendLine("     , LastName = @LastName ");
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
            sql.AppendLine("DELETE FROM Users ");
            sql.AppendLine(" WHERE Id = @Id ");
            await connection.ExecuteAsync(sql.ToString(), new { id });
        }
    }
}