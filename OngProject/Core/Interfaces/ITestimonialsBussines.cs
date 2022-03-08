
﻿using OngProject.Core.Models.DTOs;

﻿using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Entities;

namespace OngProject.Core.Interfaces
{
    public interface ITestimonialsBussines
    {
        Task<Response<TestimonialsPostToDisplayDto>> Post(TestimonialsPostDto testimonialPostDto);
        Task<Response<TestimonialsModel>> Delete(int id, string rol, string UserId);
    }
}
