using Dapper;
using DomainLayer.Data;
using DomainLayer.Entities.Users;
using RepositoryLayer.Helpers;
using System.Text;

namespace RepositoryLayer
{
    public class UserRepository : IRepository<User>
    {
        #region Property
        private DataContext _context;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAll()
        {
            using var connection = _context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM Users ");
            return await connection.QueryAsync<User>(sql.ToString());
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetById(int id)
        {
            using var connection = _context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM Users ");
            sql.AppendLine(" WHERE Id = @id ");
            return await connection.QuerySingleOrDefaultAsync<User>(sql.ToString(), new { id });
        }

        /// <summary>
        /// GetByEmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetByEmail(string email)
        {
            using var connection = _context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT * ");
            sql.AppendLine("  FROM Users ");
            sql.AppendLine(" WHERE Email = @email ");
            return await connection.QuerySingleOrDefaultAsync<User>(sql.ToString(), new { email });
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Create(User user)
        {
            using var connection = _context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new StringBuilder();
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
        /// Update
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Update(User user)
        {
            using var connection = _context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new StringBuilder();
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
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Delete(int id)
        {
            using var connection = _context.CreateConnection();
            // Build the SQL string
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DELETE FROM Users ");
            sql.AppendLine(" WHERE Id = @Id ");
            await connection.ExecuteAsync(sql.ToString(), new { id });
        }
    }
}
