namespace Trial.DTO
{
    public record HomeworkTDTO(
        int Id,
        string Homework,
        string Description,
        DateTime Deadline);

    public record AddHomeworkTDTO(
        string Homework,
        string Description,
        DateTime Deadline,
        int subjectsId
    );
    public record ModifyHomeworkTDTO(
        int Id, //
        string Homework,
        string Description,
        DateTime Deadline);


}

