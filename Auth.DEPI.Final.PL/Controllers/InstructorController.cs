using Auth.DEPI.Final.BLL.Interfaces;
using Auth.DEPI.Final.DAL.Entities;
using Auth.DEPI.Final.PL.Helpers;
using Auth.DEPI.Final.PL.ViewModels.HomeViewModels;
using Auth.DEPI.Final.PL.ViewModels.InstructorViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Auth.DEPI.Final.PL.Controllers
{
	[Authorize(Roles = "Instructor, Admin")]
	public class InstructorController : Controller
	{
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public InstructorController(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            UserManager<User> userManager

            ) {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
		public async Task<IActionResult> Index()
		{

            var user = await _userManager.GetUserAsync(User);

            
             var  courses = await _unitOfWork.CourseRepository.GetInstructorCoursesAsync(user.Id);
        
           


            //Auto Mapping

            var result = _mapper.Map<IEnumerable<CoursesViewModel>>(courses);

           

            return View(result);
        
		}
        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            
            return View();

        }

        [HttpPost]
		public async Task<IActionResult> CreateCourse(CoursesViewModel model)
		{
			var user = await _userManager.GetUserAsync(User);
			model.InstructorId = user.Id;


			// Upload the course image
			model.Thumbnail = DocumentSettings.UploadFile(model.CourseImage, "images");

			// Map the ViewModel to the entity
			var course = _mapper.Map<Courses>(model);

			// Add the course to the database
			await _unitOfWork.CourseRepository.AddAsync(course);

			// Redirect to video creation page
			return RedirectToAction("CreateVideo", new { courseId = model.Id });
		}


		[HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id is null) return BadRequest(); //400

            var course = await _unitOfWork.CourseRepository.GetAsync(id);

            if (course is null) return NotFound(); //404

            var result = _mapper.Map<CoursesViewModel>(course);

            return View(result);
        }

        [HttpPost]
        public async Task< IActionResult> Edit([FromRoute]string id,EditDeleteCourseViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    if (model.CourseImage is not null)
                    {

                        if (model.Thumbnail is not null)
                        {
                            DocumentSettings.DeleteFile(model.Thumbnail, "images");
                        }
                        model.Thumbnail = DocumentSettings.UploadFile(model.CourseImage, "images");

                    }
                    else
                    {
                        model.Thumbnail = model.Thumbnail;
                    }
                    var user = await _userManager.GetUserAsync(User);
                  

                   
                    var course = _mapper.Map<Courses>(model);


                    var count = await _unitOfWork.CourseRepository.UpdateAsync(course);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch (Exception Ex)
            {

                ModelState.AddModelError(string.Empty, Ex.Message);
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {


            if (id is null) return BadRequest(); //400

            var course = await _unitOfWork.CourseRepository.GetAsync(id);

            if (course is null) return NotFound(); //404

            var result = _mapper.Map<EditDeleteCourseViewModel>(course);

            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id, EditDeleteCourseViewModel model)
        {
            try
            {
              
                var course = _mapper.Map<Courses>(model);
                var videos = await _unitOfWork.VideoRepository.GetAllCourseVideosAsync(model.Id);

                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    if (videos.Count() > 0)
                    {
                        foreach (var item in videos)
                        {
                            DocumentSettings.DeleteFile(item.Path, "videos");
                        }
                    }
                    var count = await _unitOfWork.CourseRepository.DeleteAsync(course);
                    if (count > 0)
                    {
                        DocumentSettings.DeleteFile(model.Thumbnail, "images");
                     
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch (Exception Ex)
            {

                ModelState.AddModelError(string.Empty, Ex.Message);
            }

            return View(model);
        }
    

        [HttpGet]
        public IActionResult CreateVideo(string courseId)
        {
            var video = new VideoViewModel
            {
                CourseId = courseId
            };

            return View(video);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVideo(VideoViewModel model)
        {

           
                model.Path = DocumentSettings.UploadFile(model.CourseVideo, "videos");


                var video = _mapper.Map<Video>(model);
                    

                await _unitOfWork.VideoRepository.AddAsync(video);

                return RedirectToAction("Index");
           

          


        }


    }

}
