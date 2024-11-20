using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class RoleService: Service, IService<role, RoleModel>
{
    public RoleService(Db db) : base(db)
    {
    }

    public IQueryable<RoleModel> Query()
    {
        return _db.role.OrderBy(r => r.name).Select(r => new RoleModel() {Record = r});
    }

    public Service Create(role entity)
    {
        if (_db.role.Any(r => r.name.ToLower() == entity.name.ToLower().Trim()))
            return Error("Role already exists");
        entity.name = entity.name.Trim();
        _db.role.Add(entity);
        _db.SaveChanges();
        return Success();
    }

    public Service Update(role entity)
    {
        if (_db.role.Any(r => r.id != entity.id && r.name.ToLower() == entity.name.ToLower().Trim()))
            return Error("Role already exists");
        var rec = _db.role.SingleOrDefault(r=>r.id == entity.id);
        rec.name = entity.name;
        _db.role.Update(rec);
        _db.SaveChanges();
        return Success();
    }

    public Service Delete(int id)
    {
        var role = _db.role.Include(r => r.user).SingleOrDefault(r=>r.id== id);
        if(role is null)
            return Error("Role not found");
        if (role.user.Any())
            return Error("You role has user records.");
        
        _db.role.Remove(role);
        _db.SaveChanges();
        return Success();
    }
}