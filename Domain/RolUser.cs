namespace Domain
{
    public class RolUser
    {
        public int IdUsuarioRol {  get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public bool Estado {  get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }

        public virtual Rol? Rol { get; set; }
        public virtual User? Usuario { get; set; }
    }
}
