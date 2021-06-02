namespace DomainClass.Enums
{
	public enum UploadResult
	{
		Succeed = 0,
		EmptyFile = 1,
		Failed = 2
	}

	public enum BusinessFeatureType
	{
		Boolean = 0, 
		Number = 1
	}

	public enum SlideStatusEnum
	{
		Publish = 1,
		Draft = 0
	}
	public enum GenderEnum
	{
		Male = 1,
		Female = 0
	}

	public enum NotificationModel
	{
		Friend = 0
	}

	public enum StatusEnum
	{
		Accepted = 1,
		Rejected = 0,
		Waiting = 2
	}
	public enum VotesType
	{
		HelpFull , 
		NotHelpFull ,
		Cool , 
		Funny,
		Like  
		
	}
	public enum VotesAction
	{
		Add,
		Submission,
		Undefinded

	}

	public enum MediaType
	{
	Video , 
	Picture,
		Undefined

	}
	public enum CommunityVoteType
	{
		AddHelpFullCount = 1 , 
		AddNotHelpFullCount =2 , 
		AddHelpFullCountAndRemoveNotHelpFull = 3 , 
		AddNotHelpFullCountAndRemoveHelpFull = 4 ,
		RemoveHelpFullCount = 5 , 
		RemoveNotHelpFullCount =6 , 
		Undefined= 7


	}
}
