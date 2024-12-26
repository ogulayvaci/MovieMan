using BLL.Context;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class UserService: Service, IService<user, UserModel>
{
    public UserService(Db db) : base(db)
    {
    }

    public IQueryable<UserModel> Query()
    {
        return _db.user.Include(u => u.role).OrderByDescending(u => u.isactive).ThenBy(u=>u.username).Select(u => new UserModel() {Record = u});
    }

    public Service Create(user entity)
    {
        throw new NotImplementedException();
    }

    public Service Update(user entity)
    {
        throw new NotImplementedException();
    }

    public Service Delete(int id)
    {
        throw new NotImplementedException();
    }
}