﻿using ProyectoClinicaDSW.Models;

namespace ProyectoClinicaDSW.Repositorio
{
    public interface IHorarioMedico
    {
        IEnumerable<HorarioMedico> ListaHorarioMedico();
        string RegistrarHorario(HorarioMedico hor);

        string ActualizarHorario(HorarioMedico hor);

        string EliminarHorario(int idHorarioMedico);

        IEnumerable<HorarioMedico> FilterHorario(string nomMedico);

    }
}
