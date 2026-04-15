using BLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface ITrainerService
    {
        bool CreateTrainer(CreateTrainerViewModel createTrainer);
        bool UpdateTrainerDetails(TrainerToUpdateViewModel updatedTrainer, int trainerId);
        bool RemoveTrainer(int trainerId);
        TrainerViewModel? GetTrainerDetails(int trainerId);
        TrainerToUpdateViewModel? GetTrainerToUpdate(int trainerId);
        IEnumerable<TrainerViewModel> GetAllTrainers();
    }
}
