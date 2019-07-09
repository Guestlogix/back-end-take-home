namespace Domain.Modules.Interfaces
{
    /// <summary>
    ///     Interface to mapping csv to class.
    /// </summary>
    public interface IImportCsv
    {
        /// <summary>
        ///     Convert CSV to Class
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        object FromCsv(string[] values);
    }
}
