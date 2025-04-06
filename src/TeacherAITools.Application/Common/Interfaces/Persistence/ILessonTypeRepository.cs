using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherAITools.Application.Common.Interfaces.Persistence.Base;
using TeacherAITools.Domain.Entities;

namespace TeacherAITools.Application.Common.Interfaces.Persistence
{
    public interface ILessonTypeRepository : IRepository<LessonType>
    {
        
    }
}