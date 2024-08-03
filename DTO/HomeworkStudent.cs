namespace Trial.DTO
{
    public record AddStudentHomeworkDTO(
        int perInfoId,
        int homeworkTId,
        string Solution);

    public record StudentHomeworkDTO(
        int Id,
        int perInfoId,
        int homeworkTId,
        string Solution);

    public record ModifyStudentHomeworkDTO(
        int Id,
        string Solution);

    public record DeleteHomework(
        int homeworkTId,
        int perInfoId
    );

}