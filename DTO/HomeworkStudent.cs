namespace Trial.DTO
{
    public record AddStudentHomeworkDTO(
        int RegistrationId,
        int homeworkTId,
        string Solution);

    public record StudentHomeworkDTO(
        int Id,
        int RegistrationId,
        int homeworkTId,
        string Solution);

    public record ModifyStudentHomeworkDTO(
        int Id,
        string Solution);

    public record DeleteHomework(
        int homeworkTId,
        int RegistrationId
    );

}