using System.Collections.Generic;

namespace Trial.DTO
{
    public record SubjectRetrieving(

        String SubName,
        String Discription

    );
    public record MarkDTO(
        int Id,
        int Oral,
        int Written);

    public record HomeworkDTO(
        int Id,
        string Homework,
        DateTime Created,
        string Discription);

    public record SubjectDetails(
    int Id,
    string Name,
    IEnumerable<MarkDTO> marks,
    IEnumerable<HomeworkDTO> homeworkTs);

}