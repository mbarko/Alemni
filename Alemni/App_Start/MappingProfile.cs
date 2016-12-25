using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alemni.Models.Dtos;
using Alemni.Models.Dtos.VideoSeryViewDtos;
using Alemni.Models.Dtos.VidoSeriesViewDtos;
using AutoMapper;
using Microsoft.Owin.BuilderProperties;

namespace Alemni.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<VideoSery, VideoSeryDto>();
            Mapper.CreateMap<VideoSeryDto, VideoSery>();
            Mapper.CreateMap<VideoSery, VideoSeryViewDto>();
            Mapper.CreateMap<VideoSeryViewDto, VideoSery>();
            Mapper.CreateMap<VideoSery, VideoSeriesListItemDto>();        
            Mapper.CreateMap<VideoSeriesListItemDto, VideoSery>();
            Mapper.CreateMap<Cours, CoursDto>();
            Mapper.CreateMap<CoursDto, Cours>();
            Mapper.CreateMap<Teacher, TeacherDto>();
            Mapper.CreateMap<TeacherDto, Teacher>();
            Mapper.CreateMap<AspNetUser,AspNetUserDto>();
            Mapper.CreateMap<AspNetUserDto, AspNetUser>();
            Mapper.CreateMap<Programm, ProgrammDto>();
            Mapper.CreateMap<ProgrammDto, Programm>();
            Mapper.CreateMap<Video, VideoDto>();
            Mapper.CreateMap<VideoDto, Video>();
            Mapper.CreateMap<Video, VideoLockedDto>();
            Mapper.CreateMap<VideoDto, VideoLockedDto>();
            Mapper.CreateMap<CollectionInfo, CollectionInfoDto>();
            Mapper.CreateMap<CollectionInfoDto, CollectionInfo>();
            Mapper.CreateMap<Transaction, TransactionDto>();
            Mapper.CreateMap<TransactionDto, Transaction>();


        }
    }
}