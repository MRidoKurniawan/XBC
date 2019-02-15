using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using XBC.DataModel;
using XBC.ViewModel;

namespace XBC.Repository
{
    public class RoomRepo
    {
        public static List<RoomViewModel> All()
        {
            List<RoomViewModel> result = new List<RoomViewModel>();
            using (var db = new XBC_Context())
            {
                result = (from r in db.t_room
                          join o in db.t_office on r.office_id equals o.id
                          where (r.is_delete == false)
                          select new RoomViewModel
                          {
                              id = r.id,
                              officeId = r.office_id,
                              code = r.code,
                              name = r.name,
                              capacity = r.capacity,
                              any_projector = r.any_projector,
                              notes = r.notes,
                              isDelete = r.is_delete
                          }).ToList();
            }
            return result == null ? new List<RoomViewModel>() : result;
        }
        public static RoomViewModel ById(long id)
        {
            RoomViewModel result = new RoomViewModel();
            using (var db = new XBC_Context())
            {
                result = (from r in db.t_room
                          join o in db.t_office on r.office_id equals o.id
                          where r.id == id
                          select new RoomViewModel
                          {
                              id = r.id,
                              code = r.code,
                              name = r.name,
                              capacity = r.capacity,
                              any_projector = r.any_projector,
                              notes = r.notes,
                              isDelete = r.is_delete
                          }).FirstOrDefault();
                if (result == null)
                {
                    result = new RoomViewModel();
                }
            }
            return result;
        }
        public static ResponseResult Update(RoomViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    if (entity.id == 0)
                    {
                        t_room room = new t_room();
                        room.office_id = entity.officeId;
                        room.code = entity.code;
                        room.name = entity.name;
                        room.capacity = entity.capacity;
                        room.any_projector = entity.any_projector;
                        room.notes = entity.notes;
                        room.is_delete = false;

                        room.created_by = 1;
                        room.created_on = DateTime.Now;
                        db.t_room.Add(room);
                        db.SaveChanges();
                        entity.id = room.id;
                        result.Entity = entity;
                    }
                    else
                    {
                        t_room room = db.t_room.Where(o => o.id == entity.id).FirstOrDefault();
                        if (room != null)
                        {
                            room.code = entity.code;
                            room.name = entity.name;
                            room.capacity = entity.capacity;
                            room.any_projector = entity.any_projector;
                            room.notes = entity.notes;
                            room.is_delete = false;

                            room.modified_by = 1;
                            room.modified_on = DateTime.Now;

                            db.SaveChanges();
                            result.Entity = entity;
                        }
                        else
                        {
                            result.Success = false;
                            result.ErrorMessage = "Room not Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ResponseResult Delete(RoomViewModel entity)
        {
            ResponseResult result = new ResponseResult();
            try
            {
                using (var db = new XBC_Context())
                {
                    t_room room = db.t_room.Where(o => o.id == entity.id).FirstOrDefault();
                    if (room != null)
                    {
                        room.is_delete = true;
                        db.SaveChanges();
                        var json = new JavaScriptSerializer().Serialize(room);

                        t_audit_log log = new t_audit_log();
                        log.type = "Insert";
                        log.json_insert = json;

                        log.created_by = 1;
                        log.created_on = DateTime.Now;

                        db.t_audit_log.Add(log);

                        result.Entity = entity;
                    }
                    else
                    {
                        result.Success = false;
                        result.ErrorMessage = "Room Not Fount";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
