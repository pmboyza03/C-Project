using Microsoft.AspNetCore.Mvc;
using MovieProject.Repository;
using MovieProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace MovieProject.Controllers
{
    public class MovieController : Controller
    {
        MyProjectContext db = new MyProjectContext();
        public IActionResult Index()
        {

            var model = db.MovieTB.ToList();
            return View(model);
        }


        public string Welcome(string name, string id)
        {
            return $"Hello, Your name's' {name}. Your ID is {id}";
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MovieModel model, IFormFile fileUpload)
        //IFormFile//IFormFileคือ Interface ที่ใช้กับไฟล์ที่ทำการ Upload โดยตัวแปรที่ชื่อว่า fileUpload ต้องตรงกับหน้า Form ที่เป็น<input type="file" name="fileUpload" id="fileUpload" />
        {
            try
            {
                if (model.duration < 1)
                {
                    ModelState.AddModelError("errDuration", "The duration field is required");
                    return View();
                }
                if (fileUpload == null)
                {
                    ModelState.AddModelError("errFileUpload", "The file upload field is required.");
                    return View();
                }

                string pathImgMovie = "/images/movie/";
                string pathSave = $"wwwroot{pathImgMovie}";
                if (!Directory.Exists(pathSave)) //Directory.Exists ตรวจสอบว่ามี Directory ตามที่ระบุ path ไปหรือไม่ และสั่งสร้าง Directory ตาม path ที่ระบุ
                {
                    Directory.CreateDirectory(pathSave);
                }
                string extFile = Path.GetExtension(fileUpload.FileName);
                string fileName = DateTime.Now.ToString("dd-MM-yyyy") + extFile;
                var path = Path.Combine(Directory.GetCurrentDirectory(), pathSave, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(stream);
                }

                DateTime dateNow = DateTime.Now;
                model.coverImg = pathImgMovie + fileName;
                model.createDate = DateTime.Now.ToString();
                model.modifyDate = DateTime.Now.ToString();
                db.MovieTB.Add(model);
                int result = await db.SaveChangesAsync();
                //success
                if (result > 0)
                    return RedirectToAction("Index");

                else
                    return View(model); //กลับหน้าเดิมที่มีการแก้ไข
            }
            catch (Exception ex)
            {
                throw new Exception(message: ex.Message);
            }
            finally
            {
                //Log
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            MovieModel movie = db.MovieTB.Find(id);
            db.MovieTB.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            MovieModel movie = db.MovieTB.Find(id);
            return View(movie);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MovieModel model, IFormFile fileUpload)
        {
            try
            {
                if (model.duration < 1)
                {
                    ModelState.AddModelError("errDuration", "The duration field is required.");
                    return View();
                }
                db.MovieTB.Attach(model);
                MovieModel oldMovie = new MyProjectContext().MovieTB.Find(model.id);
                model.coverImg = oldMovie.coverImg;
                model.createDate = oldMovie.createDate;
                oldMovie = null;



                string pathImgMovie = "/images/movie/";
                string pathSave = $"wwwroot{pathImgMovie}";
                if (!Directory.Exists(pathSave))
                {
                    Directory.CreateDirectory(pathSave);
                }
                string extFile = Path.GetExtension(fileUpload.FileName);
                string fileName = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + extFile;
                var path = Path.Combine(Directory.GetCurrentDirectory(), pathSave, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(stream);
                }
                model.coverImg = pathImgMovie + fileName;


                model.modifyDate = DateTime.Now.ToString();
                db.Entry(model).State = EntityState.Modified;
                int result = await db.SaveChangesAsync();

                //success
                if (result > 0)
                    return RedirectToAction("Index");

                else
                    return View(model); //กลับหน้าเดิมที่มีการแก้ไข
            }
            catch (Exception ex)
            {
                throw new Exception(message: ex.Message);
            }
            finally
            {
                //Log
            }
        }

    }
}
