using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;

namespace BLL.Services;

public class RoleService: Service, IService<role, RoleModel>
{
    public RoleService(Db db) : base(db)
    {
    }

    public IQueryable<RoleModel> Query()
    {
        return _db.role.OrderByDescending(r => r.name).ThenBy(r => r.user).Select(r => new RoleModel() {Record = r});
    }

    public Service Create(role entity)
    {
        throw new NotImplementedException();
    }

    public Service Update(role entity)
    {
        throw new NotImplementedException();
    }

    public Service Delete(int id)
    {
        throw new NotImplementedException();
    }
}