//public static void ValidateContest(Contest contest, bool official)
//{
//    if (contest == null || contest.IsDeleted || !contest.IsVisible)
//    {
//        throw new HttpException((int)HttpStatusCode.NotFound, Resource.ContestsGeneral.Contest_not_found);
//    }

//    if (official && !contest.CanBeCompeted)
//    {
//        throw new HttpException((int)HttpStatusCode.Forbidden, Resource.ContestsGeneral.Contest_cannot_be_competed);
//    }

//    if (!official && !contest.CanBePracticed)
//    {
//        throw new HttpException((int)HttpStatusCode.Forbidden, Resource.ContestsGeneral.Contest_cannot_be_practiced);
//    }
//}

///// <summary>
///// Validates if the selected submission type from the participant is allowed in the current contest
///// </summary>
///// <param name="submissionTypeId">The id of the submission type selected by the participant</param>
///// <param name="contest">The contest in which the user participate</param>
//[NonAction]
//public static void ValidateSubmissionType(int submissionTypeId, Contest contest)
//{
//    if (contest.SubmissionTypes.All(submissionType => submissionType.Id != submissionTypeId))
//    {
//        throw new HttpException((int)HttpStatusCode.BadRequest, Resource.ContestsGeneral.Submission_type_not_found);
//    }
//}

///// <summary>
///// Displays user compete information: tasks, send source form, ranking, submissions, ranking, etc.
///// Users only.
///// </summary>
//[Authorize]
//public ActionResult Index(int id, bool official)
//{
//    var contest = this.Data.Contests.GetById(id);
//    ValidateContest(contest, official);

//    var participantFound = this.Data.Participants.Any(id, this.UserProfile.Id, official);

//    if (!participantFound)
//    {
//        if (!contest.ShouldShowRegistrationForm(official))
//        {
//            this.Data.Participants.Add(new Participant(id, this.UserProfile.Id, official));
//            this.Data.SaveChanges();
//        }
//        else
//        {
//            // Participant not found, the contest requires password or the contest has questions
//            // to be answered before registration. Redirect to the registration page.
//            // The registration page will take care of all security checks.
//            return this.RedirectToAction("Register", new { id, official });
//        }
//    }

//    var participant = this.Data.Participants.GetWithContest(id, this.UserProfile.Id, official);
//    var participantViewModel = new ParticipantViewModel(participant, official);

//    this.ViewBag.CompeteType = official ? CompeteUrl : PracticeUrl;

//    return this.View(participantViewModel);
//}

///// <summary>
///// Displays form for contest registration.
///// Users only.
///// </summary>
//[HttpGet, Authorize]
//public ActionResult Register(int id, bool official)
//{
//    var participantFound = this.Data.Participants.Any(id, this.UserProfile.Id, official);
//    if (participantFound)
//    {
//        // Participant exists. Redirect to index page.
//        return this.RedirectToAction(GlobalConstants.Index, new { id, official });
//    }

//    var contest = this.Data.Contests.All().Include(x => x.Questions).FirstOrDefault(x => x.Id == id);

//    ValidateContest(contest, official);

//    if (contest.ShouldShowRegistrationForm(official))
//    {
//        var contestRegistrationModel = new ContestRegistrationViewModel(contest, official);
//        return this.View(contestRegistrationModel);
//    }

//    var participant = new Participant(id, this.UserProfile.Id, official);
//    this.Data.Participants.Add(participant);
//    this.Data.SaveChanges();

//    return this.RedirectToAction(GlobalConstants.Index, new { id, official });
//}

///// <summary>
///// Accepts form input for contest registration.
///// Users only.
///// </summary>
////// TODO: Refactor
//[HttpPost, Authorize]
//public ActionResult Register(bool official, ContestRegistrationModel registrationData)
//{
//    // check if the user has already registered for participation and redirect him to the correct action
//    var participantFound = this.Data.Participants.Any(registrationData.ContestId, this.UserProfile.Id, official);

//    if (participantFound)
//    {
//        return this.RedirectToAction(GlobalConstants.Index, new { id = registrationData.ContestId, official });
//    }

//    var contest = this.Data.Contests.GetById(registrationData.ContestId);
//    ValidateContest(contest, official);

//    if (official && contest.HasContestPassword)
//    {
//        if (string.IsNullOrEmpty(registrationData.Password))
//        {
//            this.ModelState.AddModelError("Password", Resource.Views.CompeteRegister.Empty_Password);
//        }
//        else if (contest.ContestPassword != registrationData.Password)
//        {
//            this.ModelState.AddModelError("Password", Resource.Views.CompeteRegister.Incorrect_password);
//        }
//    }

//    if (!official && contest.HasPracticePassword)
//    {
//        if (string.IsNullOrEmpty(registrationData.Password))
//        {
//            this.ModelState.AddModelError("Password", Resource.Views.CompeteRegister.Empty_Password);
//        }
//        else if (contest.PracticePassword != registrationData.Password)
//        {
//            this.ModelState.AddModelError("Password", Resource.Views.CompeteRegister.Incorrect_password);
//        }
//    }

//    var questionsToAnswerCount = official ?
//        contest.Questions.Count(x => x.AskOfficialParticipants) :
//        contest.Questions.Count(x => x.AskPracticeParticipants);

//    if (questionsToAnswerCount != registrationData.Questions.Count())
//    {
//        this.ModelState.AddModelError("Questions", Resource.Views.CompeteRegister.Not_all_questions_answered);
//    }

//    var contestQuestions = contest.Questions.ToList();

//    var participant = new Participant(registrationData.ContestId, this.UserProfile.Id, official);
//    this.Data.Participants.Add(participant);
//    var counter = 0;
//    foreach (var question in registrationData.Questions)
//    {
//        var contestQuestion = contestQuestions.FirstOrDefault(x => x.Id == question.QuestionId);

//        var regularExpression = contestQuestion.RegularExpressionValidation;
//        bool correctlyAnswered = false;

//        if (!string.IsNullOrEmpty(regularExpression))
//        {
//            correctlyAnswered = Regex.IsMatch(question.Answer, regularExpression);
//        }

//        if (contestQuestion.Type == ContestQuestionType.DropDown)
//        {
//            int contestAnswerId;
//            if (int.TryParse(question.Answer, out contestAnswerId) && contestQuestion.Answers.Any(x => x.Id == contestAnswerId))
//            {
//                correctlyAnswered = true;
//            }

//            if (!correctlyAnswered)
//            {
//                this.ModelState.AddModelError(string.Format("Questions[{0}].Answer", counter), "Invalid selection");
//            }
//        }

//        participant.Answers.Add(new ParticipantAnswer
//        {
//            ContestQuestionId = question.QuestionId,
//            Answer = question.Answer
//        });

//        counter++;
//    }

//    if (!this.ModelState.IsValid)
//    {
//        return this.View(new ContestRegistrationViewModel(contest, registrationData, official));
//    }

//    this.Data.SaveChanges();

//    return this.RedirectToAction(GlobalConstants.Index, new { id = registrationData.ContestId, official });
//}

///// <summary>
///// Processes a participant's submission for a problem.
///// </summary>
///// <param name="participantSubmission">Participant submission.</param>
///// <param name="official">A check whether the contest is official or practice.</param>
///// <returns>Returns confirmation if the submission was correctly processed.</returns>
//[HttpPost, Authorize]
//public ActionResult Submit(SubmissionModel participantSubmission, bool official)
//{
//    var problem = this.Data.Problems.All().FirstOrDefault(x => x.Id == participantSubmission.ProblemId);
//    if (problem == null)
//    {
//        throw new HttpException((int)HttpStatusCode.Unauthorized, Resource.ContestsGeneral.Problem_not_found);
//    }

//    var participant = this.Data.Participants.GetWithContest(problem.ContestId, this.UserProfile.Id, official);
//    if (participant == null)
//    {
//        throw new HttpException((int)HttpStatusCode.Unauthorized, Resource.ContestsGeneral.User_is_not_registered_for_exam);
//    }

//    ValidateContest(participant.Contest, official);
//    ValidateSubmissionType(participantSubmission.SubmissionTypeId, participant.Contest);

//    if (this.Data.Submissions.HasSubmissionTimeLimitPassedForParticipant(participant.Id, participant.Contest.LimitBetweenSubmissions))
//    {
//        throw new HttpException((int)HttpStatusCode.ServiceUnavailable, Resource.ContestsGeneral.Submission_was_sent_too_soon);
//    }

//    if (problem.SourceCodeSizeLimit < participantSubmission.Content.Length)
//    {
//        throw new HttpException((int)HttpStatusCode.BadRequest, Resource.ContestsGeneral.Submission_too_long);
//    }

//    if (!this.ModelState.IsValid)
//    {
//        throw new HttpException((int)HttpStatusCode.BadRequest, Resource.ContestsGeneral.Invalid_request);
//    }

//    this.Data.Submissions.Add(new Submission
//    {
//        ContentAsString = participantSubmission.Content,
//        ProblemId = participantSubmission.ProblemId,
//        SubmissionTypeId = participantSubmission.SubmissionTypeId,
//        ParticipantId = participant.Id,
//        IpAddress = Request.UserHostAddress,
//    });

//    this.Data.SaveChanges();

//    return this.Json(participantSubmission.ProblemId);
//}