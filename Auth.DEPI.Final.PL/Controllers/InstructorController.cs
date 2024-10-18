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
using OnlineLearningPlatform.Models;

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

            var instructor = await _unitOfWork.InstructorRepository.GetAsync(user.Id);


			return View(instructor);
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
        public async Task<IActionResult> CreateVideo(VideoViewModel model)
        {

            if (ModelState.IsValid)
            {
                model.Path = DocumentSettings.UploadFile(model.CourseVideo, "videos");


                var video = _mapper.Map<Video>(model);
                    

                await _unitOfWork.VideoRepository.AddAsync(video);

                return View("Index");
            }

            return View();


        }
    }

}
