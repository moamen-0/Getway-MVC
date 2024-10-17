using Auth.DEPI.Final.BLL.Interfaces;
using Auth.DEPI.Final.PL.Helpers;
using Auth.DEPI.Final.PL.ViewModels.HomeViewModels;
using Auth.DEPI.Final.PL.ViewModels.InstructorViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.Models;

namespace Auth.DEPI.Final.PL.Controllers
{
	[Authorize(Roles = "Instructor, Admin")]
	public class InstructorController : Controller
	{
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public InstructorController(IMapper mapper,IUnitOfWork unitOfWork) {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
		public IActionResult Index()
		{

			
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> Create(CoursesViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Thumbnail = DocumentSettings.UploadFile(model.CourseImage, "images");
               

                var course = _mapper.Map<Courses>(model);

               
                await _unitOfWork.CourseRepository.AddAsync(course);

                return RedirectToAction("CreateVideo", new { courseId = model.Id });
            }
            return View();
            
        }
        [HttpGet]
        public IActionResult CreateVideo(string courseId)
        {
            var video = new Video
            {
                CourseId = courseId
            };

            return View(video);
        }
        [HttpPost]
        public async IActionResult CreateVideo(VideoViewModel model)
        {

            if (ModelState.IsValid)
            {
                model.Path = DocumentSettings.UploadFile(model.CourseVideo, "images");


                var video = _mapper.Map<Courses>(model);


                await _unitOfWork.CourseRepository.AddAsync(video);

                return View("Index");
            }
            return View();


        }
    }

}
