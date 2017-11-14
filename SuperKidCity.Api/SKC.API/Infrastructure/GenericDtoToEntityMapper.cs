using AutoMapper;
using SKC.Lib.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SKC.API.Models
{
    public class GenericDtoToEntityMapper : Profile
    {
        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        /// <value>The name of the profile.</value>
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        /// <summary>
        /// Override this method in a derived class and call the CreateMap method to associate that map with this profile.
        /// Avoid calling the <see cref="T:AutoMapper.Mapper" /> class from this method.
        /// </summary>
        protected override void Configure()
        {
            base.Configure();

            Mapper.Configuration.CreateMap<StateDTO, StateModel>();
               //.ForMember(dest => dest, opt => opt.MapFrom(src => src.EntityID));
            Mapper.CreateMap<FeatureFile, FileDTO>()
                .ForMember(dest => dest.EntityID, opt => opt.MapFrom(src => src.FeatureId));

            Mapper.CreateMap<NotificationRuleDTO, NotificationRule>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID));
            Mapper.CreateMap<Notification, NotificationRuleDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<FileDTO, PartFile>()
                .ForMember(dest => dest.PartRevisionId, opt => opt.MapFrom(src => src.EntityID));
            Mapper.CreateMap<PartFile, FileDTO>()
                .ForMember(dest => dest.EntityID, opt => opt.MapFrom(src => src.PartRevisionId));

            Mapper.CreateMap<FileDTO, ODFile>()
                .ForMember(dest => dest.ODId, opt => opt.MapFrom(src => src.EntityID));
            Mapper.CreateMap<ODFile, FileDTO>()
                .ForMember(dest => dest.EntityID, opt => opt.MapFrom(src => src.ODId));

            Mapper.CreateMap<DataStreamParameterValueDTO, getChecklistParameterValues_Result>().ReverseMap();

            Mapper.CreateMap<FileDTO, PartFeatureDetailsFile>()
              .ForMember(dest => dest.PartFeatureDetailsId, opt => opt.MapFrom(src => src.EntityID));

            Mapper.CreateMap<PartFeatureDetailsFile, FileDTO>()
              .ForMember(dest => dest.EntityID, opt => opt.MapFrom(src => src.PartFeatureDetailsId));

            Mapper.CreateMap<FileDTO, ProcessFile>()
                .ForMember(dest => dest.ProcessId, opt => opt.MapFrom(src => src.EntityID));
            Mapper.CreateMap<ProcessFile, FileDTO>()
                .ForMember(dest => dest.EntityID, opt => opt.MapFrom(src => src.ProcessId));

            Mapper.CreateMap<FileDTO, UserFile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.EntityID));
            Mapper.CreateMap<UserFile, FileDTO>()
                .ForMember(dest => dest.EntityID, opt => opt.MapFrom(src => src.UserId));

            Mapper.CreateMap<OperationDTO, Operation>().ReverseMap();
            Mapper.CreateMap<Page<Operation>, PageDTO<OperationDTO>>();

            Mapper.CreateMap<PartFamily, PartFamilyDTO>()
                .ForMember(dto => dto.PartFamilyType, e => e.MapFrom(o => System.Convert.ToInt32(o.PartFamilyType)))
                .ReverseMap();
            Mapper.CreateMap<Page<PartFamily>, PageDTO<PartFamilyDTO>>();

            Mapper.CreateMap<getAllOutputs, OutputPartFamilyDTO>().ReverseMap();

            Mapper.CreateMap<getPartFamiliesWithOdName, PartFamilyLiteDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PartFamilyName));

            Mapper.CreateMap<uspGetPartRecipesByPartRevisionId_Result, PartRevisionPartRecipeDTO>();

            Mapper.CreateMap<Page<uspGetFeatureList_Result>, PageDTO<FeatureDTO>>();

            Mapper.CreateMap<uspGetRolesWithUserCountAndPermission_Result, RoleEntityPermissionDTO>();
            Mapper.CreateMap<Page<uspGetRolesWithUserCountAndPermission_Result>, PageDTO<RoleEntityPermissionDTO>>();

            Mapper.CreateMap<uspGetUsersWithPermission_Result, UserEntityPermissionDTO>();
            Mapper.CreateMap<Page<uspGetUsersWithPermission_Result>, PageDTO<UserEntityPermissionDTO>>();

            Mapper.CreateMap<getDataSetFeatureList, FeatureDTO>();

            Mapper.CreateMap<getFeaturesByTags, FeatureDTO>();

            Mapper.CreateMap<getLotsByTags, LotDTO>();

            Mapper.CreateMap<getLotsByCurrentLotStatus, LotDTO>();

            Mapper.CreateMap<ChecklistValue, ChecklistValueDTO>().ReverseMap();

            Mapper.CreateMap<Page<getDataSetFeatureList>, PageDTO<FeatureDTO>>();
            Mapper.CreateMap<Page<getWorkstationList>, PageDTO<WorkstationDTO>>();
            Mapper.CreateMap<getWorkstationList, WorkstationDTO>()
               .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.ModifiedByUser }));

            Mapper.CreateMap<uspGetFeatureList_Result, FeatureDTO>()
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.CreatedByUser }))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.ModifiedByUser }));

            Mapper.CreateMap<uspGetProcessList_Result, ProcessDTO>()
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.CreatedByUser }))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.ModifiedByUser }))
                .ForMember(dest => dest.TimeZoneText, opt => opt.MapFrom(src => src.DisplayName))
                .AfterMap((src, dest) =>
                {
                    dest.ParentProcessName = (src.ParentProcessName != null) ? src.ParentProcessName : string.Empty;
                });

            Mapper.CreateMap<Page<uspGetProcessList_Result>, PageDTO<ProcessDTO>>();
            Mapper.CreateMap<getDataSetConfigValuesByCriteriaId, DataStreamParameterValueDTO>()
                .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.ParameterId))
                .ForMember(dest => dest.EntityValue, opt => opt.MapFrom(src => src.ParameterValue))
                .ForMember(dest => dest.EntityName, opt => opt.MapFrom(src => src.ParameterValueText));
            Mapper.CreateMap<DataSetCriteriaConfigValue, DataStreamParameterValueDTO>()
                .ForMember(dest => dest.EntityValue, opt => opt.MapFrom(src => src.ParameterValue))
                .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.ParameterId));

            Mapper.CreateMap<getLaunchableChecklist_Result, ChecklistParameterDTO>().ReverseMap();
            Mapper.CreateMap<getLaunchableChecklist_Result, ChecklistParameterDTO>();

            Mapper.CreateMap<getDataSetConfigValuesByCriteriaId, DataSetCriteriaConfigValueDTO>();

            Mapper.CreateMap<getControlLimitList, ControlLimitDTO>()
                .ForMember(dest => dest.PartRevision, opt => opt.MapFrom(src => new PartRevisionLiteDTO { RevisionName = src.RevisionName, Part = new PartDTO { Name = src.PartName } }))
                .ForMember(dest => dest.Process, opt => opt.MapFrom(src => new ProcessLiteDTO { Name = src.ProcessName }))
                .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => new FeatureDTO { Name = src.FeatureName }));

            Mapper.CreateMap<Page<getControlLimitList>, PageDTO<ControlLimitDTO>>();

            Mapper.CreateMap<ControlLimitDetail, ControlLimitDetailDTO>();

            Mapper.CreateMap<Page<ControlLimitDetail>, PageDTO<ControlLimitDetailDTO>>();

            Mapper.CreateMap<getDataSetProcessList, ProcessDTO>();

            Mapper.CreateMap<GroupNodeDTO, ExclusionRuleNode>()
                .ForMember(dest => dest.LogicGateType, opt => opt.MapFrom(src => (byte?)src.LogicGateType));

            Mapper.CreateMap<ExclusionRuleNode, GroupNodeDTO>()
                .ForMember(dest => dest.LogicGateType, opt => opt.MapFrom(src => (LogicGateType)src.LogicGateType));

            Mapper.CreateMap<LeafNodeDTO, ExclusionRuleNode>().ReverseMap();

            Mapper.CreateMap<ExclusionRuleNodeValueDTO, ExclusionRuleNodeRule>().ReverseMap();

            Mapper.CreateMap<Page<getDataSetProcessList>, PageDTO<ProcessDTO>>();

            Mapper.CreateMap<getDataStreamDataCollectionList, DCConfigurationDTO>();

            Mapper.CreateMap<Page<getDataStreamDataCollectionList>, PageDTO<DCConfigurationDTO>>();

            Mapper.CreateMap<getDataSetTagList, TagValueDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.TagValueId))
                .ForMember(dest => dest.TagId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.TagName, opt => opt.MapFrom(src => src.TagGroupName))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.TagValue));

            Mapper.CreateMap<Page<getDataSetTagList>, PageDTO<TagValueDTO>>();

            Mapper.CreateMap<getPartFeatureDetailsList_Result, PartFeatureDetailsDTO>()
                .ForMember(dest => dest.Feature, opt => opt.MapFrom(src => new FeatureDTO { Name = src.FeatureName }))
                .ForMember(dest => dest.Part, opt => opt.MapFrom(src => new PartDTO { Name = src.PartName }))
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.CreatedByUser }))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.ModifiedByUser }));

            Mapper.CreateMap<Page<getPartFeatureDetailsList_Result>, PageDTO<PartFeatureDetailsDTO>>();

            Mapper.CreateMap<PartFeatureDetailsDetails, PartFeatureDetailsDTO>();

            Mapper.CreateMap<PartFeatureDetails, PartFeatureDetailsDTO>()
               .AfterMap((src, dest) =>
               {
                   if (src.PartFeatureDetailsTagMaps != null && src.PartFeatureDetailsTagMaps.Count > 0)
                   {
                       List<TagValueDTO> tagValues = new List<TagValueDTO>();
                       foreach (var tagMap in src.PartFeatureDetailsTagMaps)
                       {
                           if (tagMap.TagValue != null)
                           {
                               TagValueDTO tagValue = Mapper.Map<TagValueDTO>(tagMap.TagValue);
                               tagValue.IsDeleted = tagMap.IsDeleted;
                               tagValues.Add(tagValue);
                           }
                       }

                       dest.TagValues = tagValues;
                   }
               });

            Mapper.CreateMap<PartFeatureDetailsDTO, PartFeatureDetails>();

            Mapper.CreateMap<uspGetOutputPartByODId_Result, PartRecipeOutputPartDTO>();

            Mapper.CreateMap<getAssociatedProcessDetailByUserId_Result, ProcessDTO>();
            Mapper.CreateMap<Page<getAssociatedProcessDetailByUserId_Result>, PageDTO<ProcessDTO>>();

            Mapper.CreateMap<getAssociatedProcessByGaugeDeviceId, ProcessDTO>();
            Mapper.CreateMap<Page<getAssociatedProcessByGaugeDeviceId>, PageDTO<ProcessDTO>>();

            Mapper.CreateMap<PartFamilyPartMapDTO, PartFamilyPartMap>().ReverseMap();

            Mapper.CreateMap<OperationProcessMapDTO, OperationProcessMap>().ReverseMap();

            Mapper.CreateMap<GlobalConfigurationLimitDTO, GlobalConfiguration>().ReverseMap();

            Mapper.CreateMap<TagDTO, Tag>().ReverseMap();

            Mapper.CreateMap<TagValueDTO, TagValue>()
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => new Tag { Id = src.TagId, Name = src.TagName }))
                .ReverseMap();

            Mapper.CreateMap<FeatureAssignPartDTO, FeatureAssignPart>();

            Mapper.CreateMap<ResponseGroupDTO, ResponseGroup>()
                 .ForMember(dest => dest.MultiSelect, opt => opt.MapFrom(src => src.ResponseType))
                 .ReverseMap();

            Mapper.CreateMap<ResponseGroup, ResponseGroupDTO>()
                .ForMember(dest => dest.ResponseType, opt => opt.MapFrom(src => src.MultiSelect))
                .ReverseMap();

            Mapper.CreateMap<ResponseDTO, Response>()
                .ReverseMap();

            Mapper.CreateMap<FeatureAssignPart, FeatureAssignPartDTO>()
               .ForMember(dest => dest.OperationDiagramName, opt => opt.MapFrom(src => src.PartFamily.Operation.OperationDiagram.Name))
               .ForMember(dest => dest.OperationDiagramId, opt => opt.MapFrom(src => src.PartFamily.Operation.OperationDiagram.Id))
               .ForMember(dest => dest.PartsCount, opt => opt.MapFrom(src => src.PartFamily.PartFamilyPartMaps.Count(s => !s.IsDeleted)))
               .ForMember(dest => dest.PartFamilyName, opt => opt.MapFrom(src => src.PartFamily.Name))
               .ForMember(dest => dest.RevisionName, opt => opt.MapFrom(src => !string.IsNullOrWhiteSpace(src.PartRevision.RevisionName) ? src.PartRevision.Part.Name + " (" + src.PartRevision.RevisionName + ")" : src.PartRevision.Part.Name));

            Mapper.CreateMap<FeatureInputDTO, FeatureInput>();

            Mapper.CreateMap<FeatureInput, FeatureInputDTO>()
                  .ForMember(dest => dest.InputFeatureName, opt => opt.MapFrom(src => src.Feature1.Name))
                  .ForMember(dest => dest.RevisionName, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PartRevision.RevisionName) ? src.PartRevision.Part.Name : src.PartRevision.Part.Name + " (" + src.PartRevision.RevisionName + ")"))
                  .ForMember(dest => dest.FeatureType, opt => opt.MapFrom(src => src.Feature1 != null ? src.Feature1.FeatureType : (int)InfinityQS.Marilyn.Common.FeatureType.Variable));

            Mapper.CreateMap<CalcFunctionDTO, CalcFunction>().ReverseMap();

            Mapper.CreateMap<ProcessScheduleDTO, ProcessSchedule>();

            Mapper.CreateMap<ScheduleDTO, Schedule>().ReverseMap();

            Mapper.CreateMap<ProcessSchedule, ProcessScheduleDTO>()
                .ForMember(dest => dest.ProcessName, opt => opt.MapFrom(src => src.Process.Name))
                .ForMember(dest => dest.TimeZone, opt => opt.MapFrom(src => src.Schedule.Process.TimeZoneInformation.DisplayName));

            Mapper.CreateMap<ScheduleDetailDTO, ScheduleDetail>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTimeSpan))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTimeSpan));

            Mapper.CreateMap<ScheduleDetail, ScheduleDetailDTO>()
              .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => DateTime.Today.Add(src.StartTime).ToString("hh:mm tt")))     // convert time to formated string in AM/PM like 01:55 AM
              .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => DateTime.Today.Add(src.EndTime).ToString("hh:mm tt")))     // convert time to formated string in AM/PM like 01:55 AM
              .ForMember(dest => dest.StartTimeSpan, opt => opt.Ignore())
              .ForMember(dest => dest.EndTimeSpan, opt => opt.Ignore());

            Mapper.CreateMap<DataCollectionAccessDTO, DataCollectionAccess>();

            Mapper.CreateMap<DataCollectionAccess, DataCollectionAccessDTO>()
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.RoleId != null ? src.Role.Name : (src.UserId != null ? src.User2.FullName : (src.WorkstationId != null ? src.Workstation.Name : string.Empty))))
                  .ForMember(dest => dest.HasDataCollectionPermission, opt => opt.MapFrom(src => src.RoleId != null ? src.Role.RolePermissionMaps.Select(x => x.PermissionEntityMap).Any(x => x.EntityId == (int)EntityTypes.DataCollection && x.PermissionId == (int)PermissionType.EnterData) : (src.UserId != null ? src.User2.UserRoleMaps.FirstOrDefault().Role.RolePermissionMaps.Select(x => x.PermissionEntityMap).Any(x => x.EntityId == (int)EntityTypes.DataCollection && x.PermissionId == (int)PermissionType.EnterData) : (src.WorkstationId != null ? true : false))))
                  .ForMember(dest => dest.UserCountInRole, opt => opt.MapFrom(src => src.Role.UserRoleMaps.Count()));

            Mapper.CreateMap<Feature, FeatureDTO>()
                .ForMember(dest => dest.FeatureType, opt => opt.MapFrom(src => src.FeatureType))
                .AfterMap((src, dest) =>
                {
                    if (src.FeatureTagMaps != null && src.FeatureTagMaps.Count > 0)
                    {
                        List<TagValueDTO> tagValues = new List<TagValueDTO>();
                        foreach (var tagMap in src.FeatureTagMaps)
                        {
                            if (tagMap.TagValue != null)
                            {
                                TagValueDTO tagValue = Mapper.Map<TagValueDTO>(tagMap.TagValue);
                                tagValue.IsDeleted = tagMap.IsDeleted;
                                tagValues.Add(tagValue);
                            }
                        }

                        dest.TagValues = tagValues;
                    }
                });

            Mapper.CreateMap<FeatureDTO, Feature>()
                .ForMember(dest => dest.FeatureType, opt => opt.MapFrom(src => src.FeatureType))
                .AfterMap((src, dest) =>
                {
                    if (src.TagValues != null && src.TagValues.Count() > 0)
                    {
                        foreach (var tagValue in src.TagValues)
                        {
                            if (tagValue != null)
                            {
                                FeatureTagMap featureTagMap = new FeatureTagMap() { TagValue = Mapper.Map<TagValue>(tagValue), IsDeleted = tagValue.IsDeleted };
                                dest.FeatureTagMaps.Add(featureTagMap);
                            }
                        }
                    }
                });

            Mapper.CreateMap<DataSetDTO, DataSet>()
                .ReverseMap();

            Mapper.CreateMap<Page<DataSet>, PageDTO<DataSetDTO>>();

            Mapper.CreateMap<UserPreferenceDTO, UserPreference>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreateDate))
                .ForMember(dest => dest.UserPreferenceGroupType, opt => opt.MapFrom(src => (int)src.UserPreferenceGroupType));

            Mapper.CreateMap<UserPreference, UserPreferenceDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.UserPreferenceGroupType, opt => opt.MapFrom(src => (UserPreferenceGroupType)src.UserPreferenceGroupType));

            Mapper.CreateMap<PartDTO, Part>();

            Mapper.CreateMap<Part, PartDTO>()
                .ForMember(dest => dest.PartId, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<PartRevision, PartRevisionDTO>()
                .ForMember(dest => dest.PartRevisionFiles, opt => opt.MapFrom(src => src.PartFiles))
                .AfterMap((src, dest) =>
                {
                    if (src.PartTagMaps != null && src.PartTagMaps.Count > 0)
                    {
                        List<TagValueDTO> tagValues = new List<TagValueDTO>();
                        foreach (var tagMap in src.PartTagMaps)
                        {
                            if (tagMap.TagValue != null)
                            {
                                TagValueDTO tagValue = Mapper.Map<TagValueDTO>(tagMap.TagValue);
                                tagValue.IsDeleted = tagMap.IsDeleted;
                                tagValues.Add(tagValue);
                            }
                        }

                        dest.TagValues = tagValues;
                    }
                });

            Mapper.CreateMap<PartRevisionDTO, PartRevision>()
                .ForMember(dest => dest.PartFiles, opt => opt.MapFrom(src => src.PartRevisionFiles))
                .AfterMap((src, dest) =>
                {
                    if (src.TagValues != null && src.TagValues.Count() > 0)
                    {
                        foreach (var tagValue in src.TagValues)
                        {
                            if (tagValue != null)
                            {
                                PartTagMap partTagMap = new PartTagMap() { TagValue = Mapper.Map<TagValue>(tagValue), IsDeleted = tagValue.IsDeleted };
                                dest.PartTagMaps.Add(partTagMap);
                            }
                        }
                    }
                });

            Mapper.CreateMap<PartRevision, PartRevisionLiteDTO>().ReverseMap();
            Mapper.CreateMap<PartFamilyLiteDTO, PartFamily>();
            Mapper.CreateMap<PartFamily, PartFamilyLiteDTO>()
                .ForMember(dest => dest.OperationDiagramName, opt => opt.MapFrom(src => src.Operation != null && src.Operation.OperationDiagram != null ? src.Operation.OperationDiagram.Name : string.Empty));
            Mapper.CreateMap<Page<PartFamily>, PageDTO<PartFamilyLiteDTO>>();

            Mapper.CreateMap<Operation, OperationLiteDTO>().ReverseMap();

            Mapper.CreateMap<ProcessDTO, Process>().ReverseMap();

            Mapper.CreateMap<DashboardDTO, Dashboard>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.DashboardType))
                .ReverseMap();

            Mapper.CreateMap<UserDashboardDTO, UserDashboard>().ReverseMap();

            Mapper.CreateMap<getDashboardList_Result, DashboardDTO>()
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FullName = src.CreatedByUser }))
                .AfterMap((src, dest) =>
                {
                    dest.UserDashboard = new UserDashboardDTO() { IsFavorite = src.IsFavorite };
                });

            Mapper.CreateMap<Page<getDashboardList_Result>, PageDTO<DashboardDTO>>();

            Mapper.CreateMap<DashboardLinkUrl, DashboardLinkUrlDTO>().ReverseMap();
            Mapper.CreateMap<DashboardLink, DashboardLinkDTO>().ReverseMap();
            Mapper.CreateMap<Dashboard, DashboardLinkDTO>().ReverseMap();

            Mapper.CreateMap<DashboardTile, DashboardTileDTO>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.TileType, opt => opt.MapFrom(src => (TileType)src.TileType));

            Mapper.CreateMap<DashboardTileDTO, DashboardTile>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreateDate))
                .ForMember(dest => dest.TileType, opt => opt.MapFrom(src => (short)src.TileType));

            Mapper.CreateMap<DashboardTileChartLevelDTO, DashboardTileChartLevel>().ReverseMap();

            Mapper.CreateMap<Process, ProcessDTO>()
                .ForMember(dest => dest.ProcessId, opt => opt.MapFrom(src => src.Id))
                .AfterMap((src, dest) =>
                {
                    dest.ParentProcessName = (src.ParentProcess != null) ? src.ParentProcess.Name : string.Empty;
                    dest.ParentProcessTimeZoneInformationId = (src.ParentProcess != null) ? src.ParentProcess.TimeZoneInformationId : 0;

                    if (src.ProcessTagMaps != null && src.ProcessTagMaps.Count > 0)
                    {
                        List<TagValueDTO> tagValues = new List<TagValueDTO>();
                        foreach (var tagMap in src.ProcessTagMaps)
                        {
                            if (tagMap.TagValue != null)
                            {
                                TagValueDTO tagValue = Mapper.Map<TagValueDTO>(tagMap.TagValue);
                                tagValue.IsDeleted = tagMap.IsDeleted;
                                tagValues.Add(tagValue);
                            }
                        }

                        dest.TagValues = tagValues;
                    }
                });

            Mapper.CreateMap<ProcessDTO, Process>()
                .ForMember(dest => dest.ParentProcessId, opt => opt.MapFrom(src => src.ParentProcessId))
               .AfterMap((src, dest) =>
               {
                   if (src.TagValues != null && src.TagValues.Count() > 0)
                   {
                       foreach (var tagValue in src.TagValues)
                       {
                           if (tagValue != null)
                           {
                               ProcessTagMap processTagMap = new ProcessTagMap() { TagValue = Mapper.Map<TagValue>(tagValue), IsDeleted = tagValue.IsDeleted };
                               dest.ProcessTagMaps.Add(processTagMap);
                           }
                       }
                   }
               });

            Mapper.CreateMap<OperationDiagramDTO, OperationDiagram>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OperationDiagramName))
                .AfterMap((src, dest) =>
                {
                    if (src.TagValues != null && src.TagValues.Count() > 0)
                    {
                        foreach (var tagValueDTO in src.TagValues)
                        {
                            if (tagValueDTO != null)
                            {
                                ODTagMap tagMap = new ODTagMap() { TagValue = Mapper.Map<TagValue>(tagValueDTO), IsDeleted = tagValueDTO.IsDeleted, ModifiedBy = src.ModifiedBy, CreatedBy = src.CreatedBy, CreateDate = src.CreateDate, ModifiedDate = src.ModifiedDate, TagValueId = tagValueDTO.ID };
                                dest.ODTagMaps.Add(tagMap);
                            }
                        }
                    }
                });

            Mapper.CreateMap<OperationDiagram, OperationDiagramDTO>()
                .ForMember(dest => dest.OperationDiagramName, opt => opt.MapFrom(src => src.Name))
                .AfterMap((src, dest) =>
                {
                    if (src.ODTagMaps != null && src.ODTagMaps.Count > 0)
                    {
                        List<TagValueDTO> tagValues = new List<TagValueDTO>();
                        foreach (var tagMap in src.ODTagMaps)
                        {
                            if (tagMap.TagValue != null)
                            {
                                TagValueDTO tagValue = Mapper.Map<TagValueDTO>(tagMap.TagValue);
                                tagValue.IsDeleted = tagMap.IsDeleted;
                                tagValue.CreateDate = tagMap.CreateDate;
                                tagValue.CreatedBy = tagMap.CreatedBy;
                                tagValue.ModifiedDate = tagMap.ModifiedDate;
                                tagValue.ModifiedBy = tagMap.ModifiedBy;
                                tagValues.Add(tagValue);
                            }
                        }

                        dest.TagValues = tagValues;
                    }
                });

            Mapper.CreateMap<DCConfigurationDTO, DCConfiguration>().ReverseMap();

            Mapper.CreateMap<DCConfiguration, DCConfigurationDTO>()
                .ForMember(dest => dest.OperationDiagramName, opt => opt.MapFrom(src => src.Operation.OperationDiagram.Name));

            Mapper.CreateMap<DCConfigurationTagMap, DCConfigurationTagMapDTO>()
                .ForMember(dest => dest.Tag, opt => opt.MapFrom(src => src.Tag))
                .ForMember(dest => dest.AssociationLevel, opt => opt.MapFrom(src => src.AssociationLevel));

            Mapper.CreateMap<DCConfigurationTagMapDTO, DCConfigurationTagMap>()
                .ForMember(dest => dest.AssociationLevel, opt => opt.MapFrom(src => src.AssociationLevel));

            Mapper.CreateMap<DCConfiguration, DCConfigurationSettingDTO>()
                 .ForMember(dest => dest.DCConfigurationTagMap, opt => opt.MapFrom(src => src.DCConfigurationTagMaps)).ReverseMap();

            Mapper.CreateMap<FileTypeDTO, FileType>()
                .ReverseMap();

            ////Mapper.CreateMap<FileDTO, DCFile>()
            ////    .ForMember(dest => dest.DCConfigurationId, opt => opt.MapFrom(src => src.EntityID))
            ////    .ForMember(dest => dest.DCFileName, opt => opt.MapFrom(src => src.FileName))
            ////    .ReverseMap();

            Mapper.CreateMap<FileDTO, FeatureFile>()
                .ForMember(dest => dest.FeatureId, opt => opt.MapFrom(src => src.EntityID));

            Mapper.CreateMap<FeatureFile, FileDTO>()
                .ForMember(dest => dest.EntityID, opt => opt.MapFrom(src => src.FeatureId));

            Mapper.CreateMap<FileDTO, ProcessFile>()
                .ForMember(dest => dest.ProcessId, opt => opt.MapFrom(src => src.EntityID))
                .ReverseMap();

            Mapper.CreateMap<FileDTO, PartFeatureDetailsFile>()
              .ForMember(dest => dest.PartFeatureDetailsId, opt => opt.MapFrom(src => src.EntityID))
              .ReverseMap();

            Mapper.CreateMap<FileDTO, ODFile>()
                .ForMember(dest => dest.ODId, opt => opt.MapFrom(src => src.EntityID))
                .ReverseMap();

            Mapper.CreateMap<CodeTagMapDTO, CodeTagMap>().ReverseMap();

            Mapper.CreateMap<CodeGroupDTO, CodeGroup>().ReverseMap();

            Mapper.CreateMap<CodeGroupTagMapDTO, CodeGroupTagMap>().ReverseMap();

            Mapper.CreateMap<ResponseGroupDTO, ResponseGroup>().ReverseMap();

            Mapper.CreateMap<PageDTO<ResponseGroupDTO>, Page<ResponseGroup>>().ReverseMap();

            Mapper.CreateMap<ChecklistDTO, Checklist>().ReverseMap();

            Mapper.CreateMap<ChecklistDTO, Checklist>()
             .AfterMap((src, dest) =>
             {
                 if (src.TagValues != null && src.TagValues.Count() > 0)
                 {
                     foreach (var tagValue in src.TagValues)
                     {
                         if (tagValue != null)
                         {
                             ChecklistTagMap checklistTagMap = new ChecklistTagMap() { TagValue = Mapper.Map<TagValue>(tagValue), IsDeleted = tagValue.IsDeleted };
                             dest.ChecklistTagMaps.Add(checklistTagMap);
                         }
                     }
                 }
             });

            Mapper.CreateMap<Checklist, ChecklistDTO>().AfterMap((src, dest) =>
            {
                this.MapChecklistAccesses(src, dest);
                this.MapChecklistFeatures(src, dest);
                this.MapChecklistProcesses(src, dest);
                this.MapChecklistTags(src, dest);
            });

            Mapper.CreateMap<ChecklistDTO, Checklist>();

            Mapper.CreateMap<ChecklistAccess, ChecklistAccessMapDTO>();

            Mapper.CreateMap<ChecklistFeatureMap, ChecklistFeatureMapDTO>()
                .ForMember(dest => dest.FeatureName, opt => opt.MapFrom(src => src.Feature.Name))
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.Feature.Description))
                .ForMember(dest => dest.ResponseType, opt => opt.MapFrom(src => src.Feature.ResponseGroup.MultiSelect));

            Mapper.CreateMap<PageDTO<ChecklistDTO>, Page<Checklist>>().ReverseMap();

            Mapper.CreateMap<SubGroupFeatureValueDTO, SubGroupFeatureValue>();

            Mapper.CreateMap<SubGroupFeatureValue, SubGroupFeatureValueDTO>()
             .ForMember(dest => dest.PartId, opt => opt.Ignore())
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.CodeId, opt => opt.MapFrom(src => src.CodeId))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy));

            Mapper.CreateMap<SubGroupFeatureDTO, SubGroupFeature>();

            Mapper.CreateMap<SubGroupFeature, SubGroupFeatureDTO>()
                .ForMember(dest => dest.FeatureType, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FeatureId, opt => opt.MapFrom(src => src.FeatureId))
                .ForMember(dest => dest.SampleSize, opt => opt.MapFrom(src => src.SampleSize))
                .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.ModifiedBy));

            Mapper.CreateMap<SubGroupPieceDTO, SubGroupPiece>();

            Mapper.CreateMap<SubGroupPiece, SubGroupPieceDTO>()
                .ForMember(dest => dest.PartId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PartSNId, opt => opt.MapFrom(src => src.PartSNId));

            Mapper.CreateMap<SubGroupDTO, SubGroup>();

            Mapper.CreateMap<SubGroup, SubGroupDTO>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.ProcessId, opt => opt.MapFrom(src => src.ProcessId))
                 .ForMember(dest => dest.PartRevisionId, opt => opt.MapFrom(src => src.PartRevisionId))
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ModifiedBy))
                 .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp));

            Mapper.CreateMap<SubGroupTagDTO, SubGroupTag>().ReverseMap();

            Mapper.CreateMap<SubGroupFeatureTagMapDTO, SubGroupFeatureTagMap>().ReverseMap();

            Mapper.CreateMap<SubGroupPieceTagDTO, SubGroupPieceTag>().ReverseMap();

            Mapper.CreateMap<RoleDTO, Role>();

            Mapper.CreateMap<Role, RoleDTO>()
                .ForMember(dest => dest.PermissionEntityMaps, opt => opt.MapFrom(src => src.RolePermissionMaps.Select(x => x.PermissionEntityMap)));

            Mapper.CreateMap<PermissionEntityMap, PermissionEntityMapDTO>()
               .ForMember(dest => dest.RolePermissionMapId, opt => opt.MapFrom(src => src.RolePermissionMaps.FirstOrDefault(x => !x.IsDeleted).Id))
               .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.RolePermissionMaps.FirstOrDefault(x => !x.IsDeleted).CreateDate))
               .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.RolePermissionMaps.FirstOrDefault(x => !x.IsDeleted).CreatedBy))
               .ForMember(dest => dest.ModifiedDate, opt => opt.MapFrom(src => src.RolePermissionMaps.FirstOrDefault(x => !x.IsDeleted).ModifiedDate))
               .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => src.RolePermissionMaps.FirstOrDefault(x => !x.IsDeleted).ModifiedBy));

            Mapper.CreateMap<PermissionEntityMapDTO, PermissionEntityMap>();

            Mapper.CreateMap<EntityDTO, Entity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.EntityName));
            Mapper.CreateMap<Entity, EntityDTO>()
                .ForMember(dest => dest.EntityName, opt => opt.MapFrom(src => src.Name)).ReverseMap();
            Mapper.CreateMap<PermissionDTO, Permission>().ReverseMap();

            Mapper.CreateMap<DataCollectionDTO, DataCollection>().ReverseMap();

            Mapper.CreateMap<SpecificationLimit, SpecificationLimitDTO>()
            .ForMember(dest => dest.FeatureName, opt => opt.MapFrom(src => src.Feature.Name))
            .ForMember(dest => dest.PartName, opt => opt.MapFrom(src => src.PartRevision.Part.Name))
            .ForMember(dest => dest.PartRevisionName, opt => opt.MapFrom(src => src.PartRevision.RevisionName))
            .ForMember(dest => dest.LotName, opt => opt.MapFrom(src => src.Lot.Name))
            .ForMember(dest => dest.ProcessName, opt => opt.MapFrom(src => src.Process.Name))
            .ForMember(dest => dest.MeasurementUnitText, opt => opt.MapFrom(src => src.MeasurementUnit.MeasurementUnit1))
            .AfterMap((s, d) =>
            {
                if (s.SpecificationLimitPieceValue != null)
                {
                    d.PieceLimit = new PieceLimitDTO()
                    {
                        LowerSpecificationLimit = s.SpecificationLimitPieceValue.LowerSpecificationLimit,
                        UpperSpecificationLimit = s.SpecificationLimitPieceValue.UpperSpecificationLimit,
                        LowerWarningLimit = s.SpecificationLimitPieceValue.LowerWarningLimit,
                        UpperWarningLimit = s.SpecificationLimitPieceValue.UpperWarningLimit,
                        LowerReasonableLimit = s.SpecificationLimitPieceValue.LowerReasonableLimit,
                        UpperReasonableLimit = s.SpecificationLimitPieceValue.UpperReasonableLimit,
                        Target = s.SpecificationLimitPieceValue.Target,
                        AlternateInputTypeId = (OtherInputOptionType)s.SLInputOptionType,
                        SpecificationLimitId = s.SpecificationLimitPieceValue.SpecificationLimitId
                    };
                    d.SpecialLimit = new SpecialLimitDTO()
                    {
                        LowerWithinPieceLimit = s.SpecificationLimitPieceValue.LowerWithinPieceLimit,
                        UpperWithinPieceLimit = s.SpecificationLimitPieceValue.UpperWithinPieceLimit,
                        LowerSubgroupLimit = s.SpecificationLimitPieceValue.LowerSubgroupLimit,
                        UpperSubgroupLimit = s.SpecificationLimitPieceValue.UpperSubgroupLimit,
                        SpecificationLimitId = s.SpecificationLimitPieceValue.SpecificationLimitId
                    };
                }
            });

            Mapper.CreateMap<SpecificationLimitDTO, SpecificationLimit>().ForMember(dest => dest.SLInputOptionType, opt => opt.MapFrom(src => src.PieceLimit.AlternateInputTypeId))
                .AfterMap((s, d) =>
                {
                    if (s.PieceLimit != null && s.SpecialLimit != null)
                    {
                        // PieceLimits
                        d.SpecificationLimitPieceValue = new SpecificationLimitPieceValue()
                        {
                            UpperSpecificationLimit = s.PieceLimit.UpperSpecificationLimit,
                            Target = s.PieceLimit.Target,
                            LowerSpecificationLimit = s.PieceLimit.LowerSpecificationLimit,
                            UpperWarningLimit = s.PieceLimit.UpperWarningLimit,
                            LowerWarningLimit = s.PieceLimit.LowerWarningLimit,
                            UpperReasonableLimit = s.PieceLimit.UpperReasonableLimit,
                            LowerReasonableLimit = s.PieceLimit.LowerReasonableLimit,

                            // Special Limits
                            UpperSubgroupLimit = s.SpecialLimit.UpperSubgroupLimit,
                            LowerSubgroupLimit = s.SpecialLimit.LowerSubgroupLimit,
                            UpperWithinPieceLimit = s.SpecialLimit.UpperWithinPieceLimit,
                            LowerWithinPieceLimit = s.SpecialLimit.LowerWithinPieceLimit,
                            SpecificationLimitId = s.PieceLimit.SpecificationLimitId
                        };
                    }
                });
            Mapper.CreateMap<MAVMethod, MAVMethodDTO>().ReverseMap();
            Mapper.CreateMap<T1T2Method, T1T2MethodDTO>().ReverseMap();
            Mapper.CreateMap<SpecificationLimitPieceValue, PieceLimitDTO>().ReverseMap();

            Mapper.CreateMap<MeasurementUnit, MeasurementUnitDTO>()
                .ForMember(dest => dest.MeasurementUnit, opt => opt.MapFrom(src => src.MeasurementUnit1))
                .ReverseMap();

            Mapper.CreateMap<PartRecipeDetailDTO, PartRecipeDetail>().ReverseMap();

            Mapper.CreateMap<PartRecipeDTO, PartRecipe>()
                .ForMember(dest => dest.PartRecipeDetails, opt => opt.MapFrom(src => src.PartRecipeDetails));

            Mapper.CreateMap<PartRecipe, PartRecipeDTO>()
                .ForMember(dest => dest.PartRecipeDetails, opt => opt.MapFrom(src => src.PartRecipeDetails));

            Mapper.CreateMap<PageDTO<MeasurementUnitDTO>, Page<MeasurementUnit>>().ReverseMap();

            // Specific mappings for PageDTO and Page
            Mapper.CreateMap<PageDTO<PartDTO>, Page<Part>>().ReverseMap();
            Mapper.CreateMap<PageDTO<PartRevisionDTO>, Page<PartRevision>>().ReverseMap();

            Mapper.CreateMap<Page<uspGetPartList_Result>, PageDTO<PartRevisionDTO>>();
            Mapper.CreateMap<uspGetPartList_Result, PartRevisionDTO>()
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.CreatedByUser }))
                .ForMember(dest => dest.PartId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.PartRevisionID))
                .ForMember(dest => dest.Part, opt => opt.MapFrom(src => new PartDTO { Name = src.Name }))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.ModifiedByUser }));

            Mapper.CreateMap<PageDTO<ProcessDTO>, Page<Process>>().ReverseMap();
            Mapper.CreateMap<PageDTO<OperationDiagramDTO>, Page<OperationDiagram>>()
                .ReverseMap();

            Mapper.CreateMap<PageDTO<PartRecipeDTO>, Page<PartRecipe>>().ReverseMap();
            Mapper.CreateMap<PageDTO<FeatureDTO>, Page<Feature>>()
                .ReverseMap();
            Mapper.CreateMap<PageDTO<SpecificationLimitDTO>, Page<SpecificationLimit>>().ReverseMap();
            Mapper.CreateMap<PageDTO<DCConfigurationDTO>, Page<DCConfiguration>>().ReverseMap();
            Mapper.CreateMap<PageDTO<TagDTO>, Page<Tag>>().ReverseMap();

            Mapper.CreateMap<DCFeatureConfiguration, DCFeatureConfigurationDTO>()
                .ForMember(dest => dest.GaugeValueSelectionType, opt => opt.MapFrom(src => ((GaugeValueSelectionType?)src.GaugeValueSelectionType)));

            Mapper.CreateMap<DCFeatureConfigurationDTO, DCFeatureConfiguration>()
                .ForMember(dest => dest.DCConfiguration, opt => opt.Ignore())
                .ForMember(dest => dest.GaugeValueSelectionType, opt => opt.MapFrom(src => ((int?)src.GaugeValueSelectionType)));

            Mapper.CreateMap<PageDTO<DCFeatureConfigurationDTO>, Page<DCFeatureConfiguration>>().ReverseMap();
            Mapper.CreateMap<PageDTO<RawDataDTO>, Page<uspGetRawDataForChart>>().ReverseMap();

            Mapper.CreateMap<RawDataDTO, uspGetRawDataForChart>().ReverseMap();

            Mapper.CreateMap<FeatureAssignmentDTO, FeatureAssignment>();

            Mapper.CreateMap<FeatureAssignment, FeatureAssignmentDTO>();

            Mapper.CreateMap<Page<FeatureAssignment>, PageDTO<FeatureAssignmentDTO>>();

            Mapper.CreateMap<ManualDCDetails, ManualDCPartProcessDTO>();

            Mapper.CreateMap<ManualDCDetailsForSubgroup, ManualDCPartProcessDTO>();

            Mapper.CreateMap<ShiftDTO, Shift>().ReverseMap();
            Mapper.CreateMap<ShiftList, ShiftDTO>().ReverseMap();
            Mapper.CreateMap<Page<ShiftList>, PageDTO<ShiftDTO>>().ReverseMap();
            Mapper.CreateMap<ShiftProcessScheduleDTO, getScheduledProcessesByShiftWithUserAccessLevel>().ReverseMap();
            Mapper.CreateMap<Page<Shift>, PageDTO<ShiftDTO>>();
            Mapper.CreateMap<DelinkedDCConfigDTO, delinkPartFamily>().ReverseMap();
            Mapper.CreateMap<LotDTO, Lot>()
                 .AfterMap((src, dest) =>
                 {
                     if (src.TagValues != null && src.TagValues.Count() > 0)
                     {
                         foreach (var tagValue in src.TagValues)
                         {
                             if (tagValue != null)
                             {
                                 LotTagMap lotTagMap = new LotTagMap() { TagValue = Mapper.Map<TagValue>(tagValue), IsDeleted = tagValue.IsDeleted };
                                 dest.LotTagMaps.Add(lotTagMap);
                             }
                         }
                     }
                 });
            Mapper.CreateMap<Lot, LotDTO>()
                 .ForMember(dest => dest.PartName, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.PartRevision.RevisionName) ? src.PartRevision.Part.Name : src.PartRevision.Part.Name + " (" + src.PartRevision.RevisionName + ")"))
                  .AfterMap((src, dest) =>
                  {
                      if (src.LotTagMaps != null && src.LotTagMaps.Count > 0)
                      {
                          List<TagValueDTO> tagValues = new List<TagValueDTO>();
                          foreach (var tagMap in src.LotTagMaps)
                          {
                              if (tagMap.TagValue != null)
                              {
                                  TagValueDTO tagValue = Mapper.Map<TagValueDTO>(tagMap.TagValue);
                                  tagValue.IsDeleted = tagMap.IsDeleted;
                                  tagValues.Add(tagValue);
                              }
                          }

                          dest.TagValues = tagValues;
                      }
                  });
            Mapper.CreateMap<LotStatusDetail, LotStatusDetailDTO>()
                .ForMember(dest => dest.MeasurementUnitText, opt => opt.MapFrom(src => src.MeasurementUnit.MeasurementUnit1))
                .ForMember(dest => dest.ProcessName, opt => opt.MapFrom(src => src.Process.Name))
                 .AfterMap((src, dest) =>
                 {
                     dest.QuantityWithUnit = src.Quantity + " " + dest.MeasurementUnitText;
                 });
            Mapper.CreateMap<LotStatusDetailDTO, LotStatusDetail>();
            Mapper.CreateMap<LotTagMap, LotTagMapDTO>().ReverseMap();

            Mapper.CreateMap<PartDerivationQuestion, PartDerivationQuestionDTO>().ReverseMap();
            Mapper.CreateMap<PartDerivationQuestion, PartFamilyLiteDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.PartFamilyName))
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.PartFamilyId))
                .ReverseMap();
            Mapper.CreateMap<PartDerivationQuestionnaire, PartDerivationQuestionnaireDTO>().ReverseMap();
            Mapper.CreateMap<PartProcessMap, PartProcessMapDTO>().ReverseMap();
            Mapper.CreateMap<uspGetLinkedPRsForRollup_Result, LinkedODPartRecipeInfoDTO>().ReverseMap();
            Mapper.CreateMap<uspGetLinkedPartRecipesForRollDown_Result, LinkedODPartRecipeInfoDTO>().ReverseMap();
            Mapper.CreateMap<PageDTO<CodeGroupDTO>, Page<CodeGroup>>().ReverseMap();
            Mapper.CreateMap<PageDTO<PartRevisionDTO>, Page<PartRevision>>().ReverseMap();
            Mapper.CreateMap<CodeGroup, CodeGroupDTO>().ReverseMap();
            Mapper.CreateMap<CodeGroupTagMap, CodeGroupTagMapDTO>().ReverseMap();
            Mapper.CreateMap<PageDTO<CodeDTO>, Page<Code>>().ReverseMap();
            Mapper.CreateMap<Code, CodeDTO>().ReverseMap();
            Mapper.CreateMap<PageDTO<TagValueDTO>, Page<TagValue>>().ReverseMap();
            Mapper.CreateMap<EntityDependencyDTO, getTagMapping_Result>().ReverseMap();
            Mapper.CreateMap<EntityAssociationMapDTO, uspGetFeatureDependentEntityNames_Result>().ReverseMap();
            Mapper.CreateMap<EntityAssociationMapDTO, getRuleTemplateDependencies>().ReverseMap();
            Mapper.CreateMap<EntityAssociationMapDTO, getProcessingTemplateDependencies>().ReverseMap();
            Mapper.CreateMap<getFeatureAssociatedEntityNames, EntityAssociationMapDTO>();
            Mapper.CreateMap<EntityAssociationMapDTO, getDataSetDependentEntityNames>().ReverseMap();
            Mapper.CreateMap<EntityAssociationMapDTO, getShiftAssociations_Result>().ReverseMap();
            Mapper.CreateMap<EntityAssociationMapDTO, getShiftProcessAssociations_Result>().ReverseMap();
            Mapper.CreateMap<getAssociatedDataCollectionByPartRevisionId_Result, DCConfigurationDTO>();
            Mapper.CreateMap<getAssociatedOperationDiagramByPartRevisionId_Result, OperationDiagramDTO>()
                .ForMember(dest => dest.OperationDiagramName, opt => opt.MapFrom(src => src.Name));
            Mapper.CreateMap<getAssociatedPartRecipeByPartRevisionId_Result, PartRecipeOutputPartDTO>();
            Mapper.CreateMap<uspGetProcessDependentEntityNames_Result, EntityDependencyDTO>();
            Mapper.CreateMap<EntityAssociationMapDTO, uspGetPartRevisionDependentEntityNames_Result>().ReverseMap();
            Mapper.CreateMap<uspGetODWithSingleOutputByRevisionId_Result, OperationDiagramDTO>();
            Mapper.CreateMap<OperationDiagramAssociationMapDTO, getImpactedAssociationsForOperationDiagram_Result>()
                .ReverseMap();
            Mapper.CreateMap<TagValueDTO, Tag>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TagName))
                .ReverseMap();
            Mapper.CreateMap<TagValue, TagValueDTO>()
                .ForMember(dest => dest.TagName, opt => opt.MapFrom(src => src.Tag.Name))
                .ReverseMap();

            Mapper.CreateMap<DataSetTagMap, DataSetTagMapDTO>()
                .AfterMap((src, dest) =>
                {
                    dest.TagValue.TagName = src.TagValue.Tag.Name;
                });
            Mapper.CreateMap<PageDTO<UserDTO>, Page<User>>()
                .ReverseMap();
            Mapper.CreateMap<PageDTO<WorkstationDTO>, Page<Workstation>>()
                .ReverseMap();

            Mapper.CreateMap<PageDTO<ProcessStateLogDTO>, Page<ProcessStateLog>>()
              .ReverseMap();

            Mapper.CreateMap<ProcessStateLog, ProcessStateLogDTO>().ReverseMap();

            Mapper.CreateMap<StateLog, StateLogDTO>().ReverseMap();

            Mapper.CreateMap<User, UserDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                ////.ForMember(dest => dest.UserConfigureTagValues, opt => opt.MapFrom(src => src.UserTagValueMaps))
                .ForMember(dest => dest.Supervisor, opt => opt.MapFrom(src => src.SupervisorUser))
                .ForMember(dest => dest.UserFiles, opt => opt.MapFrom(src => src.UserFiles1))
                .ForMember(dest => dest.ActivationStartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.ActivationEndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserRoleMapId, opt => opt.MapFrom(src => src.UserRoleMaps.Select(x => x.Id).FirstOrDefault()))
                .AfterMap((src, dest) =>
                {
                    if (src.UserTagValueMaps2 != null && src.UserTagValueMaps2.Count > 0)
                    {
                        List<TagValueDTO> tagValues = new List<TagValueDTO>();
                        foreach (var tagValueMap in src.UserTagValueMaps2)
                        {
                            if (tagValueMap != null && tagValueMap.TagValue != null)
                            {
                                TagValueDTO tagValue = Mapper.Map<TagValueDTO>(tagValueMap.TagValue);
                                tagValue.IsDeleted = tagValueMap.IsDeleted;
                                tagValues.Add(tagValue);
                            }

                            dest.UserConfigureTagValues = tagValues;
                        }
                    }
                });

            Mapper.CreateMap<UserDTO, User>()
                .ForMember(dest => dest.SupervisorUser, opt => opt.MapFrom(src => src.Supervisor))
                .ForMember(dest => dest.UserFiles1, opt => opt.MapFrom(src => src.UserFiles))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.ActivationStartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.ActivationEndDate))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
                .AfterMap((src, dest) =>
                {
                    if (src.UserConfigureTagValues != null && src.UserConfigureTagValues.Count() > 0)
                    {
                        foreach (var tagValue in src.UserConfigureTagValues)
                        {
                            if (tagValue != null)
                            {
                                UserTagValueMap userTagValueMap = new UserTagValueMap() { TagValue = Mapper.Map<TagValue>(tagValue), IsDeleted = tagValue.IsDeleted, CreateDate = tagValue.CreateDate, CreatedBy = tagValue.CreatedBy, ModifiedBy = tagValue.ModifiedBy, ModifiedDate = tagValue.ModifiedDate };
                                dest.UserTagValueMaps2.Add(userTagValueMap);
                            }
                        }
                    }
                });

            Mapper.CreateMap<getUsersByTags, UserDTO>().ReverseMap();

            Mapper.CreateMap<UserSetting, UserSettingDTO>().ReverseMap();

            Mapper.CreateMap<UserAccessLevel, UserAccessLevelDTO>()
                .ForMember(dest => dest.ProcessName, opt => opt.MapFrom(src => src.Process != null ? src.Process.Name : string.Empty));

            Mapper.CreateMap<UserAccessLevelDTO, UserAccessLevel>();

            Mapper.CreateMap<EntityTagMapDTO, EntityTagMap>()
                .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.Entity));

            Mapper.CreateMap<EntityTagMap, EntityTagMapDTO>()
                .ForMember(dest => dest.Entity, opt => opt.MapFrom(src => src.EntityId))
                .ReverseMap();

            Mapper.CreateMap<Tenant, TenantDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => Convert.ToInt64(src.Id)))
                .ForMember(dest => dest.LicensesInfo, opt => opt.MapFrom(src => src.LicenseInfoes));

            Mapper.CreateMap<TenantDTO, Tenant>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Convert.ToInt32(src.ID)))
               .ForMember(dest => dest.LicenseInfoes, opt => opt.MapFrom(src => src.LicensesInfo));

            Mapper.CreateMap<Themes, ThemeDTO>()
                .ForMember(dest => dest.ThemeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsDefaultTheme, opt => opt.MapFrom(src => src.IsDefault));

            Mapper.CreateMap<Font, FontDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<TenantDeployment, TenantDeploymentDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<TenantDeploymentDTO, TenantDeployment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID));

            Mapper.CreateMap<Deployment, DeploymentDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.WebURL, opt => opt.MapFrom(src => new Uri(src.WebURL)))
                .ForMember(dest => dest.IdentityURL, opt => opt.MapFrom(src => new Uri(src.IdentityURL)));

            Mapper.CreateMap<DeploymentDTO, Deployment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.WebURL, opt => opt.MapFrom(src => src.WebURL.AbsoluteUri))
                .ForMember(dest => dest.IdentityURL, opt => opt.MapFrom(src => src.IdentityURL.AbsoluteUri));

            Mapper.CreateMap<UserAccount, UserAccountDTO>()
                .ForMember(dest => dest.UserPassword, opt => opt.Ignore()).ReverseMap();

            Mapper.CreateMap<UserAccessLevelDTO, UserAccessLevel>().ReverseMap();

            Mapper.CreateMap<SecurityConfiguration, SecurityConfigurationDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<SecurityConfigurationDTO, SecurityConfiguration>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID));

            Mapper.CreateMap<UserAccount_History, UserAccountHistoryDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.HistoryId))
                .ReverseMap();

            Mapper.CreateMap<PageDTO<RoleDTO>, Page<Role>>().ReverseMap();

            Mapper.CreateMap<User, EntityDependencyDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ReverseMap();

            Mapper.CreateMap<EntityDependencyDTO, getRoleAssociations_Result>().ReverseMap();

            Mapper.CreateMap<getODRemoveImpactList_Result, EntityDependencyDTO>().ReverseMap();
            Mapper.CreateMap<getOperationRemoveImpactList, EntityDependencyDTO>().ReverseMap();

            Mapper.CreateMap<Page<getOperationDiagramList>, PageDTO<OperationDiagramListDTO>>();

            Mapper.CreateMap<getOperationDiagramList, OperationDiagramListDTO>()
                  .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.CreatedByUser }))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.ModifiedByUser }))
                .ForMember(dest => dest.InputPartFamilyCount, opt => opt.MapFrom(src => src.InputPartFamilyCount));

            Mapper.CreateMap<uspGetSubGroupData, SubGroupDataDTO>()
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.TIME))
                .ReverseMap();

            ////Mapper.CreateMap<NotificationDTO, Notification>()
            ////    .ForMember(dest => dest.IsEmail, opt => opt.MapFrom(src => src.NotificationEmails != null && src.NotificationEmails.Count() > 0));

            ////Mapper.CreateMap<Notification, NotificationDTO>();

            ////Mapper.CreateMap<NotificationEmailDTO, NotificationEmail>().ReverseMap();
            ////Mapper.CreateMap<NotificationEmailDispatchDTO, NotificationEmailDispatch>().ReverseMap();
            Mapper.CreateMap<InfinityQS.Marilyn.Context.Master.Language, LanguageDTO>().ReverseMap();

            Mapper.CreateMap<Page<uspGetSpecificationLimits_Result>, PageDTO<SpecificationLimitListDTO>>();

            Mapper.CreateMap<uspGetSpecificationLimits_Result, SpecificationLimitListDTO>().ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.CreatedByUser }))
                                                                                            .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.ModifiedByUser }));

            Mapper.CreateMap<FeatureDCAssociationDTO, getFeatureDCLinkage_Result>().ReverseMap();
            Mapper.CreateMap<DCPartFamilyDTO, getFeatureDCAssociationDetail_Result>().ReverseMap();

            Mapper.CreateMap<StatisticalRule, StatisticalRuleDTO>()
                .ForMember(dest => dest.StatisticalControlRuleType, opt => opt.MapFrom(src => (StatisticalControlRuleType)src.RuleTypeId));
            Mapper.CreateMap<StatisticalRuleDTO, StatisticalRule>()
                .ForMember(dest => dest.RuleTypeId, opt => opt.MapFrom(src => (byte?)src.StatisticalControlRuleType));

            Mapper.CreateMap<StatisticalRuleTemplateDTO, StatisticalRuleTemplate>().ReverseMap();
            Mapper.CreateMap<StatisticalRuleTemplateMap, StatisticalRuleTemplateMapDTO>().ReverseMap();

            Mapper.CreateMap<StatisticalRule, StatisticalRuleDTO>();

            Mapper.CreateMap<StatisticalRuleDTO, StatisticalRuleDTO>();

            Mapper.CreateMap<Page<StatisticalRuleTemplate>, PageDTO<StatisticalRuleTemplateDTO>>();

            Mapper.CreateMap<ProcessState, ProcessStateDTO>().ReverseMap();
            Mapper.CreateMap<ProcessStateLog, ProcessStateLogDTO>().ReverseMap();
            Mapper.CreateMap<getLinkedDCConfigDetailByPRID_Result, DCConfigFeatureCountDTO>()
                .ForMember(dst => dst.ID, conf => conf.MapFrom(src => Convert.ToInt32(src.ID, CultureInfo.InvariantCulture)));
            Mapper.CreateMap<Page<getLinkedDCConfigDetailByPRID_Result>, PageDTO<DCConfigFeatureCountDTO>>();
            Mapper.CreateMap<LicenseInfo, LicenseInfoDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            Mapper.CreateMap<OTP, OTPDTO>().ReverseMap();
            Mapper.CreateMap<License, LicenseDTO>().ReverseMap();
            Mapper.CreateMap<LicenseList, LicenseDTO>()
              .ReverseMap();
            Mapper.CreateMap<Workstation, WorkstationDTO>().ReverseMap();
            Mapper.CreateMap<getCodeGroupDependentEntityNames, EntityDependencyDTO>();
            Mapper.CreateMap<getCodeDependentEntityNames, EntityDependencyDTO>();
            Mapper.CreateMap<getMeasurementUnitDependentEntityNames, EntityDependencyDTO>();
            Mapper.CreateMap<Page<getDataSetList_Result>, PageDTO<DataSetDTO>>();
            Mapper.CreateMap<Page<LicenseList>, PageDTO<LicenseDTO>>();
            Mapper.CreateMap<Page<License>, PageDTO<LicenseDTO>>();
            Mapper.CreateMap<getDataSetList_Result, DataSetDTO>()
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.CreatedByUser }))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.ModifiedByUser }));
            Mapper.CreateMap<BusinessConditionDTO, Condition>().ReverseMap();
            Mapper.CreateMap<PageDTO<BusinessConditionDTO>, Page<Condition>>().ReverseMap();
            Mapper.CreateMap<GroupNodeDTO, ConditionNode>()
                .ForMember(dest => dest.LogicGateType, opt => opt.MapFrom(src => (byte?)src.LogicGateType));

            Mapper.CreateMap<ConditionNode, GroupNodeDTO>()
                .ForMember(dest => dest.LogicGateType, opt => opt.MapFrom(src => (LogicGateType)src.LogicGateType));

            Mapper.CreateMap<ConditionNodeRule, ProcessStateRuleDTO>()
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ConditionNodeRuleValues)).ReverseMap();
            Mapper.CreateMap<ConditionNodeRule, ShiftRuleDTO>()
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ConditionNodeRuleValues)).ReverseMap();
            Mapper.CreateMap<ConditionNodeRule, TimeRuleDTO>().ReverseMap();

            Mapper.CreateMap<TimeValueDTO, ConditionNodeTimeRuleValue>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTimeSpan))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTimeSpan));

            Mapper.CreateMap<ConditionNodeTimeRuleValue, TimeValueDTO>()
              .ForMember(dest => dest.StartTimeSpan, opt => opt.MapFrom(src => src.StartTime))
              .ForMember(dest => dest.EndTimeSpan, opt => opt.MapFrom(src => src.EndTime));

            Mapper.CreateMap<ConditionNodeRule, DayOfWeekRuleDTO>().ReverseMap();
            Mapper.CreateMap<ConditionNodeDayRuleValue, DayOfWeekRuleValueDTO>().ReverseMap();

            Mapper.CreateMap<LeafNodeDTO, ConditionNode>().ReverseMap();

            Mapper.CreateMap<DCRequirementDTO, DCRequirement>()
               .ForMember(dest => dest.DCRequirementProcesses, opt => opt.MapFrom(src => src.RequirementProcesses));

            Mapper.CreateMap<DCRequirement, DCRequirementDTO>()
             .ForMember(dest => dest.RequirementProcesses, opt => opt.MapFrom(src => src.DCRequirementProcesses));

            Mapper.CreateMap<DCRequirementDTO, DCRequirement>()
               .ForMember(dest => dest.ReferenceType, opt => opt.MapFrom(src => (byte?)src.ReferenceType));

            Mapper.CreateMap<DCRequirement, DCRequirementDTO>()
                .ForMember(dest => dest.ReferenceType, opt => opt.MapFrom(src => src.ReferenceType == null ? (ReferenceType?)null : (ReferenceType)src.ReferenceType));

            Mapper.CreateMap<DCRequirementDTO, DCRequirement>()
            .ForMember(dest => dest.FrequencyTimeIntervalUnit, opt => opt.MapFrom(src => (byte?)src.FrequencyTimeIntervalUnit));

            Mapper.CreateMap<DCRequirement, DCRequirementDTO>()
                .ForMember(dest => dest.FrequencyTimeIntervalUnit, opt => opt.MapFrom(src => src.FrequencyTimeIntervalUnit == null ? (FrequencyIntervalUnitType?)null : (FrequencyIntervalUnitType)src.FrequencyTimeIntervalUnit));

            Mapper.CreateMap<DCRequirementDTO, DCRequirement>()
                .ForMember(dest => dest.WindowLateUnit, opt => opt.MapFrom(src => (byte?)src.WindowLateUnit));

            Mapper.CreateMap<DCRequirement, DCRequirementDTO>()
                .ForMember(dest => dest.WindowLateUnit, opt => opt.MapFrom(src => src.WindowLateUnit == null ? (FrequencyIntervalUnitType?)null : (FrequencyIntervalUnitType)src.WindowLateUnit));

            Mapper.CreateMap<DCRequirementDTO, DCRequirement>()
               .ForMember(dest => dest.WindowEarlyReminderUnit, opt => opt.MapFrom(src => (byte?)src.WindowEarlyReminderUnit));

            Mapper.CreateMap<DCRequirement, DCRequirementDTO>()
                .ForMember(dest => dest.WindowEarlyReminderUnit, opt => opt.MapFrom(src => src.WindowEarlyReminderUnit == null ? (FrequencyIntervalUnitType?)null : (FrequencyIntervalUnitType)src.WindowEarlyReminderUnit));

            Mapper.CreateMap<DCRequirementEventDTO, DCRequirementEvent>()
             .ForMember(dest => dest.WindowEarlyReminderUnit, opt => opt.MapFrom(src => (byte?)src.WindowEarlyReminderUnit));

            Mapper.CreateMap<DCRequirementEvent, DCRequirementEventDTO>()
                .ForMember(dest => dest.WindowEarlyReminderUnit, opt => opt.MapFrom(src => src.WindowEarlyReminderUnit == null ? (FrequencyIntervalUnitType?)null : (FrequencyIntervalUnitType)src.WindowEarlyReminderUnit));

            Mapper.CreateMap<DCRequirementEventDTO, DCRequirementEvent>()
                .ForMember(dest => dest.WindowLateUnit, opt => opt.MapFrom(src => (byte?)src.WindowLateUnit));

            Mapper.CreateMap<DCRequirementEvent, DCRequirementEventDTO>()
                .ForMember(dest => dest.WindowLateUnit, opt => opt.MapFrom(src => src.WindowLateUnit == null ? (FrequencyIntervalUnitType?)null : (FrequencyIntervalUnitType)src.WindowLateUnit));

            Mapper.CreateMap<DCRequirementEventDTO, DCRequirementEvent>()
                .ForMember(dest => dest.WindowDeadlineUnit, opt => opt.MapFrom(src => (byte?)src.WindowDeadlineUnit));

            Mapper.CreateMap<DCRequirementEvent, DCRequirementEventDTO>()
                .ForMember(dest => dest.WindowDeadlineUnit, opt => opt.MapFrom(src => src.WindowDeadlineUnit == null ? (FrequencyIntervalUnitType?)null : (FrequencyIntervalUnitType)src.WindowDeadlineUnit));

            Mapper.CreateMap<DCDueStatusResult, DataCollectionDueStatusDTO>()
                .ForMember(dest => dest.WindowLateUnit, opt => opt.MapFrom(src => src.WindowLateUnit == null ? (FrequencyIntervalUnitType?)null : (FrequencyIntervalUnitType)src.WindowLateUnit));

            Mapper.CreateMap<DCDueStatusResult, DataCollectionDueStatusDTO>()
                .ForMember(dest => dest.WindowEarlyReminderUnit, opt => opt.MapFrom(src => src.WindowEarlyReminderUnit == null ? (FrequencyIntervalUnitType?)null : (FrequencyIntervalUnitType)src.WindowEarlyReminderUnit));

            Mapper.CreateMap<UsersWorkstationsForDC, DataCollectionPublishEventDTO>()
                .ForMember(dest => dest.WindowLateUnit, opt => opt.MapFrom(src => src.WindowLateUnit == null ? (FrequencyIntervalUnitType?)null : (FrequencyIntervalUnitType)src.WindowLateUnit));

            Mapper.CreateMap<UsersWorkstationsForDC, DataCollectionPublishEventDTO>()
                .ForMember(dest => dest.WindowEarlyReminderUnit, opt => opt.MapFrom(src => src.WindowEarlyReminderUnit == null ? (FrequencyIntervalUnitType?)null : (FrequencyIntervalUnitType)src.WindowEarlyReminderUnit));

            Mapper.CreateMap<UsersWorkstationsForDC, DataCollectionPublishEventDTO>()
                .ForMember(dest => dest.WindowDeadlineUnit, opt => opt.MapFrom(src => src.WindowDeadlineUnit == null ? (FrequencyIntervalUnitType?)null : (FrequencyIntervalUnitType)src.WindowDeadlineUnit));

            Mapper.CreateMap<GroupNodeDTO, DCRequirementNode>()
                .ForMember(dest => dest.LogicGateType, opt => opt.MapFrom(src => (byte?)src.LogicGateType));

            Mapper.CreateMap<LeafNodeDTO, DCRequirementNode>().ReverseMap();

            Mapper.CreateMap<DCRequirementNode, GroupNodeDTO>()
                .ForMember(dest => dest.LogicGateType, opt => opt.MapFrom(src => (LogicGateType)src.LogicGateType));

            Mapper.CreateMap<RequirementProcessDTO, DCRequirementProcess>()
                .ForMember(dest => dest.DCRequirementId, opt => opt.MapFrom(src => src.RequirementId));
            Mapper.CreateMap<DCRequirementProcess, RequirementProcessDTO>()
                .ForMember(dest => dest.RequirementId, opt => opt.MapFrom(src => src.DCRequirementId));

            Mapper.CreateMap<PageDTO<DCRequirementDTO>, Page<DCRequirement>>().ReverseMap();
            Mapper.CreateMap<DataSetDTO, DataSet>().ReverseMap();
            Mapper.CreateMap<DataSetTagMapDTO, DataSetTagMap>()
                .ReverseMap();
            Mapper.CreateMap<getProcessesByUserAccessLevel, ProcessDTO>();

            Mapper.CreateMap<ControlLimitDTO, ControlLimit>().ReverseMap();

            Mapper.CreateMap<ControlLimitDetail, ControlLimitDetailDTO>().ReverseMap();

            Mapper.CreateMap<getAllProcessesWithUserAccessLevel, ProcessDTO>().ForMember(dest => dest.TimeZoneText, opt => opt.MapFrom(src => src.DisplayName));

            Mapper.CreateMap<getProcessesByOperationAndUser, ProcessDTO>();
            Mapper.CreateMap<getProcessesByOperationsAndUser_Result, ProcessDTO>();
            Mapper.CreateMap<getProcessesByTags, ProcessDTO>();
            Mapper.CreateMap<getProcessesByOperationsAndUser_Result, ProcessLiteDTO>();
            Mapper.CreateMap<getProcessesByTags, ProcessLiteDTO>();
            Mapper.CreateMap<getSitesByUserAndDataSetId, ProcessLiteDTO>();
            Mapper.CreateMap<Process, ProcessLiteDTO>().ReverseMap();
            Mapper.CreateMap<getConditionDependentEntityNames, EntityDependencyDTO>();

            Mapper.CreateMap<User, UserLiteDTO>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<UserLiteDTO, User>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID));
            Mapper.CreateMap<Page<getTagList_Result>, PageDTO<TagDTO>>();
            Mapper.CreateMap<getTagList_Result, TagDTO>().ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.CreatedByUser }))
                                                                                            .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.ModifiedByUser }))
                                                                                            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TagName))
                                                                                            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TagType));
            Mapper.CreateMap<DataSetCriteriaConfig, DataStreamParameterDTO>()
                .ForMember(dest => dest.OperatorType, opt => opt.MapFrom(src => src.OperatorType))
                .ForMember(dest => dest.ParameterType, opt => opt.MapFrom(src => (byte)src.ParameterType))
                .ForMember(dest => dest.DataStreamParameterValues, opt => opt.MapFrom(src => src.DataSetCriteriaConfigValues));

            Mapper.CreateMap<DataStreamParameterDTO, DataSetCriteriaConfig>()
                .ForMember(dest => dest.OperatorType, opt => opt.MapFrom(src => (byte)src.OperatorType))
                .ForMember(dest => dest.ParameterType, opt => opt.MapFrom(src => (int)src.ParameterType))
                .ForMember(dest => dest.DataSetCriteriaConfigValues, opt => opt.MapFrom(src => src.DataStreamParameterValues));

            Mapper.CreateMap<DataSetCriteriaConfigValue, DataSetCriteriaConfigValueDTO>().ReverseMap();
            Mapper.CreateMap<DataSetCriteriaConfigValueDTO, DataStreamParameterValueDTO>()
                .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.ParameterId))
                .ForMember(dest => dest.EntityValue, opt => opt.MapFrom(src => src.ParameterValue));
            Mapper.CreateMap<DataStreamParameterValueDTO, DataSetCriteriaConfigValue>()
                .ForMember(dest => dest.ParameterId, opt => opt.MapFrom(src => src.EntityId))
                .ForMember(dest => dest.ParameterValue, opt => opt.MapFrom(src => src.EntityValue));

            Mapper.CreateMap<DataSetTimeConfig, DataSetTimeConfigDTO>().ReverseMap();

            Mapper.CreateMap<DataSet, ChartDataSetDTO>()
            .ForMember(dest => dest.DataSetId, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.DataSetName, opt => opt.MapFrom(src => src.Name));
            Mapper.CreateMap<GaugeFormatCommandDTO, GaugeFormatCommand>().ReverseMap();
            Mapper.CreateMap<GaugeFormatRecordDetailDTO, GaugeFormatRecordDetail>().ReverseMap();
            Mapper.CreateMap<Page<GaugeInterface>, PageDTO<GaugeInterfaceDTO>>();
            Mapper.CreateMap<GaugeInterfaceDTO, GaugeInterface>().ReverseMap();
            Mapper.CreateMap<GaugeInterfaceList, GaugeInterfaceDTO>().ReverseMap();
            Mapper.CreateMap<Page<GaugeInterfaceList>, PageDTO<GaugeInterfaceDTO>>();
            Mapper.CreateMap<GaugeInterfaceCommandDTO, GaugeInterfaceCommand>().ReverseMap();
            Mapper.CreateMap<GaugeFormatList, GaugeFormatDTO>().ReverseMap();
            Mapper.CreateMap<Page<GaugeFormatList>, PageDTO<GaugeFormatDTO>>();
            Mapper.CreateMap<Page<GaugeFormat>, PageDTO<GaugeFormatDTO>>();
            Mapper.CreateMap<GaugeDeviceDTO, GaugeDevice>().ReverseMap();
            Mapper.CreateMap<Page<GaugeDevice>, PageDTO<GaugeDeviceDTO>>();
            Mapper.CreateMap<LotList, LotDTO>()
                .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.CreatedByUser }))
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.ModifiedByUser }))
                .AfterMap((src, dest) =>
                {
                    dest.QuantityWithUnit = src.Quantity + " " + src.MeasurementUnit;
                });

            Mapper.CreateMap<Page<LotList>, PageDTO<LotDTO>>();
            Mapper.CreateMap<Page<Lot>, PageDTO<LotDTO>>();
            Mapper.CreateMap<LotDependentEntityNames, EntityDependencyDTO>().ReverseMap();
            Mapper.CreateMap<Page<DataSet>, PageDTO<ChartDataSetDTO>>()
               .AfterMap((src, dest) =>
               {
                   if (src.DataList != null && src.DataList.Count() > 0)
                   {
                       foreach (var data in src.DataList)
                       {
                           ChartDataSetDTO chartDataSet = new ChartDataSetDTO() { DataSetName = data.Name, DataSetId = data.Id };
                           dest.DataList.ToList().Add(chartDataSet);
                       }
                   }
               });

            Mapper.CreateMap<GaugeFormatDTO, GaugeFormat>()
               .ForMember(dest => dest.FormatType, opt => opt.MapFrom(src => src.FormatType));

            Mapper.CreateMap<GaugeFormat, GaugeFormatDTO>()
                 .ForMember(dest => dest.FormatType, opt => opt.MapFrom(src => src.FormatType));

            Mapper.CreateMap<ComPortSettingDTO, ComPortSetting>();
            Mapper.CreateMap<ComPortSetting, ComPortSettingDTO>();
            Mapper.CreateMap<TCPIPSettingDTO, TCPIPSetting>().ReverseMap();
            Mapper.CreateMap<GaugeInterfaceConnectionDTO, GaugeInterfaceConnection>().ReverseMap();
            Mapper.CreateMap<GaugeDeviceAssociationDTO, GaugeInterfaceConnectionAssociation>().ReverseMap();
            Mapper.CreateMap<GaugeDeviceProcessMapDTO, GaugeDeviceProcessMap>().ReverseMap();
            Mapper.CreateMap<GaugeDeviceProcessMap, ProcessDTO>().ReverseMap();
            Mapper.CreateMap<Page<GaugeDeviceList>, PageDTO<GaugeDeviceDTO>>().ReverseMap();
            Mapper.CreateMap<GaugeDeviceList, GaugeDeviceDTO>()
                .AfterMap((src, dest) =>
                {
                    if (src.ComPortSettingId != null && src.ComPortSettingId > 0)
                    {
                        dest.GaugeInterfaceConnection.ComPortSettingId = src.ComPortSettingId;
                        dest.GaugeInterfaceConnection.ComPortSetting.ID = src.ComPortSettingId.HasValue ? src.ComPortSettingId.Value : 0;
                        dest.GaugeInterfaceConnection.ComPortSetting.BaudRate = (BaudRateType)src.BaudRate;
                        dest.GaugeInterfaceConnection.ComPortSetting.BufferSize = (BufferSizeType)src.BufferSize;
                        dest.GaugeInterfaceConnection.ComPortSetting.AgentWorkstationPortMap.PortName = src.PortName;
                        dest.GaugeInterfaceConnection.ComPortSetting.DataBits = (DataBitsType)src.DataBits;
                        dest.GaugeInterfaceConnection.ComPortSetting.FlowControl = (FlowControlType)src.FlowControl;
                        dest.GaugeInterfaceConnection.ComPortSetting.Parity = (ParityType)src.Parity;
                        dest.GaugeInterfaceConnection.ComPortSetting.StopBits = (StopBitsType)src.StopBits;
                    }
                    else
                    {
                        dest.GaugeInterfaceConnection.TCPIPSettingId = src.TCPIPSettingId;
                        dest.GaugeInterfaceConnection.TCPIPSetting.ID = src.TCPIPSettingId.HasValue ? src.TCPIPSettingId.Value : 0;
                        dest.GaugeInterfaceConnection.TCPIPSetting.Host = src.Host;
                        dest.GaugeInterfaceConnection.TCPIPSetting.IPAddress = src.IPAddress;
                    }

                    dest.GaugeInterfaceConnection.GaugeInterfaceName = src.GaugeInterfaceName;
                    dest.GaugeInterfaceConnection.AgentWorkstation.Name = src.Workstation;
                });

            Mapper.CreateMap<Page<getGaugeInterfaceConnectionList_Result>, PageDTO<GaugeInterfaceConnectionDTO>>().ReverseMap();
            Mapper.CreateMap<getGaugeInterfaceConnectionList_Result, GaugeInterfaceConnectionDTO>()
                .AfterMap((src, dest) =>
                {
                    if (src.ComPortSettingId != null && src.ComPortSettingId > 0)
                    {
                        dest.ComPortSettingId = src.ComPortSettingId;
                        dest.ComPortSetting.ID = src.ComPortSettingId.HasValue ? src.ComPortSettingId.Value : 0;
                        dest.ComPortSetting.BaudRate = (BaudRateType)src.BaudRate;
                        dest.ComPortSetting.BufferSize = (BufferSizeType)src.BufferSize;
                        dest.ComPortSetting.AgentWorkstationPortMap.PortName = src.PortName;
                        dest.ComPortSetting.AgentWorkstationPortMapId = src.AgentWorkstationPortMapId.Value;
                        dest.ComPortSetting.AgentWorkstationPortMap.ID = src.AgentWorkstationPortMapId.Value;
                        dest.ComPortSetting.DataBits = (DataBitsType)src.DataBits;
                        dest.ComPortSetting.FlowControl = (FlowControlType)src.FlowControl;
                        dest.ComPortSetting.Parity = (ParityType)src.Parity;
                        dest.ComPortSetting.StopBits = (StopBitsType)src.StopBits;
                        dest.TCPIPSetting = null;
                        dest.TCPIPSettingId = null;
                    }
                    else
                    {
                        dest.TCPIPSettingId = src.TCPIPSettingId;
                        dest.TCPIPSetting.ID = src.TCPIPSettingId.HasValue ? src.TCPIPSettingId.Value : 0;
                        dest.TCPIPSetting.Host = src.Host;
                        dest.TCPIPSetting.IPAddress = src.IPAddress;
                        dest.ComPortSetting = null;
                        dest.ComPortSettingId = null;
                    }

                    dest.GaugeInterfaceId = src.GaugeInterfaceId;
                    dest.GaugeInterfaceName = src.GaugeInterfaceName;
                    dest.AgentWorkstation.ID = src.WorkstationId;
                    dest.AgentWorkstation.Name = src.WorkstationName;
                    dest.GaugeDevicesCount = src.GaugeDeviceCount.Value;
                });

            Mapper.CreateMap<DCDueStatusResult, DataCollectionDueStatusDTO>()
                .ForMember(dest => dest.DCEventId, opt => opt.MapFrom(src => (DCEventType)src.DCEventType))
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(src => (EntityTypes)src.EntityType));
            Mapper.CreateMap<Page<DCDueStatusResult>, PageDTO<DataCollectionDueStatusDTO>>();

            Mapper.CreateMap<PageDTO<ASCIICode>, Page<ASCIICodeDTO>>().ReverseMap();
            Mapper.CreateMap<ASCIICodeDTO, ASCIICode>().ReverseMap();
            Mapper.CreateMap<DataSetBuilderReturnedDataDTO, DataSetBuilderReturnedData>().ReverseMap();
            Mapper.CreateMap<DCRequirementEventDTO, DCRequirementEvent>().ReverseMap();
            Mapper.CreateMap<DCRequirementEvent, DataCollectionStatusDTO>()
                .ForMember(dest => dest.DataCollectionName, opt => opt.MapFrom(src => src.DCConfiguration.Name))
                .ForMember(dest => dest.DCRequirementName, opt => opt.MapFrom(src => src.DCRequirement.Name))
                .ForMember(dest => dest.ProcessName, opt => opt.MapFrom(src => src.Process.Name))
                .ForMember(dest => dest.DCEventId, opt => opt.MapFrom(src => (DCEventType)src.DCEventType));

            Mapper.CreateMap<StatisticalRule, StatisticalRuleDTO>()
                .ForMember(dest => dest.StatisticalControlRuleType, opt => opt.MapFrom(src => (Common.StatisticalControlRuleType)src.RuleTypeId));

            Mapper.CreateMap<StatisticalRulesByTemplateIdResult, StatisticalRuleDTO>()
                .ForMember(dest => dest.StatisticalControlRuleType, opt => opt.MapFrom(src => (Common.StatisticalControlRuleType)src.RuleTypeId))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.X, opt => opt.MapFrom(src => src.X))
                .ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.Y))
                .ForMember(dest => dest.VariabilityTypeId, opt => opt.MapFrom(src => Enum.GetName(typeof(Common.VariabilityType), src.VariabilityTypeId)));

            Mapper.CreateMap<getStatisticalRulesByTemplateIdVariableType, StatisticalRuleDTO>()
           .ForMember(dest => dest.StatisticalControlRuleType, opt => opt.MapFrom(src => (Common.StatisticalControlRuleType)src.RuleTypeId))
           .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
           .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
           .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.X, opt => opt.MapFrom(src => src.X))
           .ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.Y))
           .ForMember(dest => dest.VariabilityTypeId, opt => opt.MapFrom(src => Enum.GetName(typeof(Common.VariabilityType), src.VariabilityTypeId)));

            Mapper.CreateMap<StatisticalRulesByTemplateIdResult, StatisticalRuleAssignmentDTO>()
                .ForMember(dest => dest.VariabilityType, opt => opt.MapFrom(src => Enum.GetName(typeof(Common.VariabilityType), src.VariabilityTypeId)))
                .ForMember(dest => dest.StatisticalRule, opt => opt.MapFrom(src => Mapper.Map<StatisticalRuleDTO>(src)));

            Mapper.CreateMap<getStatisticalRulesByTemplateIdVariableType, StatisticalRuleAssignmentDTO>()
               .ForMember(dest => dest.VariabilityType, opt => opt.MapFrom(src => Enum.GetName(typeof(Common.VariabilityType), src.VariabilityTypeId)))
               .ForMember(dest => dest.StatisticalRule, opt => opt.MapFrom(src => Mapper.Map<StatisticalRuleDTO>(src)));

            Mapper.CreateMap<Page<getStatisticalRulesByTemplateIdVariableType>, PageDTO<StatisticalRuleAssignmentDTO>>();

            Mapper.CreateMap<ProcessingTemplateResult, PostProcessingFactorDTO>()
                .ForMember(dest => dest.PostProcessingType, opt => opt.MapFrom(src => (PostProcessingType)src.PostProcessingOptionId))
                .ForMember(dest => dest.F1, opt => opt.MapFrom(src => src.F1))
                .ForMember(dest => dest.F2, opt => opt.MapFrom(src => src.F2))
                .ForMember(dest => dest.F3, opt => opt.MapFrom(src => src.F3))
                .ForMember(dest => dest.F4, opt => opt.MapFrom(src => src.F4))
                .ForMember(dest => dest.F5, opt => opt.MapFrom(src => src.F5));

            Mapper.CreateMap<ProcessingTemplateResult, ProcessingTemplateDTO>()
                .ForMember(dest => dest.NormalizationType, opt => opt.MapFrom(src => (Common.NormalizationType)src.NormalizationOptions))
                .ForMember(dest => dest.FeatureType, opt => opt.MapFrom(src => (Common.ProcessingTemplateFeatureType)src.FeatureType))
                .ForMember(dest => dest.PostProcessingFactor, opt => opt.MapFrom(src => src.PostProcessingOptionId == null ? null : Mapper.Map<PostProcessingFactorDTO>(src)));

            Mapper.CreateMap<ControlLimitDetailsResult, ControlLimitDetailDTO>()
                .ForMember(dest => dest.ControlLimitId, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<SpecificationViolationResult, StreamViolationEvent>()
                .ForMember(dest => dest.PieceNumber, opt => opt.MapFrom(src => Convert.ToInt32(src.PieceNumber)))
                .ForMember(dest => dest.UpperLimit, opt => opt.MapFrom(src => src.Limit.UpperLimit))
                .ForMember(dest => dest.LowerLimit, opt => opt.MapFrom(src => src.Limit.LowerLimit))
                .ForMember(dest => dest.ViolationTypeId, opt => opt.MapFrom(src => (int)src.ViolationType));
            Mapper.CreateMap<NetContentViolationResult, StreamViolationEvent>()
                .ForMember(dest => dest.PieceNumber, opt => opt.MapFrom(src => Convert.ToInt32(src.PieceNumber)))
                .ForMember(dest => dest.UpperLimit, opt => opt.MapFrom(src => src.Limit.UpperLimit))
                .ForMember(dest => dest.LowerLimit, opt => opt.MapFrom(src => src.Limit.LowerLimit))
                .ForMember(dest => dest.ViolationTypeId, opt => opt.MapFrom(src => (int)src.ViolationType));
            Mapper.CreateMap<StatisticalViolationResult, StreamViolationEvent>()
                .ForMember(dest => dest.PieceNumber, opt => opt.MapFrom(src => Convert.ToInt32(src.PieceNumber)))
                .ForMember(dest => dest.ViolationTypeId, opt => opt.MapFrom(src => (int)src.ViolationType))
                .ForMember(dest => dest.StatisticalRuleId, opt => opt.MapFrom(src => src.Rule.ID))
                .ForMember(dest => dest.X, opt => opt.MapFrom(src => src.Rule.X))
                .ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.Rule.Y))
                .ForMember(dest => dest.VariabilityTypeId, opt => opt.MapFrom(src => (int)src.Rule.VariabilityTypeId));
            Mapper.CreateMap<InvalidDataViolationResult, StreamViolationEvent>()
                .ForMember(dest => dest.PieceNumber, opt => opt.MapFrom(src => Convert.ToInt32(src.PieceNumber)))
                .ForMember(dest => dest.ViolationTypeId, opt => opt.MapFrom(src => (int)src.ViolationType))
                .ForMember(dest => dest.VariabilityTypeId, opt => opt.MapFrom(src => (int)src.VariabilityType))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ViolationType == ViolationType.InvalidProcessMean ? src.ProcessMean : src.ViolationType == ViolationType.InvalidProcessSD ? src.ProcessSD : src.WithinPieceSD));

            Mapper.CreateMap<getDataSetShiftList, ShiftDTO>();

            Mapper.CreateMap<Page<getDataSetShiftList>, PageDTO<ShiftDTO>>();

            Mapper.CreateMap<getSubgroupsByDataSetId, SubGroupDataDTO>();

            Mapper.CreateMap<getSubGroupsForControlLimit, SubGroupDataDTO>()
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.SampleSize));

            Mapper.CreateMap<ChartSubGroupDataDTO, ChartSubGroupLiteDTO>();

            Mapper.CreateMap<NotificationRuleDTO, NotificationRule>()
                .ForMember(dest => dest.NotificationRuleParameters, opt => opt.MapFrom(src => src.DataStreamParameters));

            Mapper.CreateMap<NotificationRule, NotificationRuleDTO>()
              .ForMember(dest => dest.DataStreamParameters, opt => opt.MapFrom(src => src.NotificationRuleParameters));

            Mapper.CreateMap<NotificationRuleRecipientDTO, NotificationRuleRecipient>();

            Mapper.CreateMap<NotificationRule, NotificationRuleDTO>()
            .ForMember(dest => dest.NotificationRuleRecipients, opt => opt.MapFrom(src => src.NotificationRuleRecipients));

            Mapper.CreateMap<NotificationRule, NotificationRuleDTO>()
              .ForMember(dest => dest.DataStreamParameters, opt => opt.MapFrom(src => src.NotificationRuleParameters));

            Mapper.CreateMap<PageDTO<NotificationRuleDTO>, Page<NotificationRule>>().ReverseMap();

            Mapper.CreateMap<DataStreamParameterDTO, NotificationRuleParameter>()
                .ForMember(dest => dest.NotificationRuleId, opt => opt.MapFrom(src => src.DataStreamConfigurationId))
                .ForMember(dest => dest.NotificationRuleParameterValues, opt => opt.MapFrom(src => src.DataStreamParameterValues));

            Mapper.CreateMap<NotificationRuleParameter, DataStreamParameterDTO>()
               .ForMember(dest => dest.DataStreamConfigurationId, opt => opt.MapFrom(src => src.NotificationRuleId))
               .ForMember(dest => dest.DataStreamParameterValues, opt => opt.MapFrom(src => src.NotificationRuleParameterValues));

            Mapper.CreateMap<DataStreamParameterValueDTO, NotificationRuleParameterValue>()
                .ForMember(dest => dest.NotificationRuleParameterId, opt => opt.MapFrom(src => src.DataStreamParameterId)).ReverseMap();

            Mapper.CreateMap<ApplicationClient, ApplicationClientDTO>()
              .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.AuthorizationScopes, opt => opt.MapFrom(src => src.ApplicationClientAuthorizationScopeMaps));

            Mapper.CreateMap<ApplicationClientAuthorizationScopeMap, AuthorizationScopeDTO>()
                .ForMember(dest => dest.AuthorizationScopeName, opt => opt.MapFrom(src => src.AuthorizationScope.AuthorizationScopeName))
                .ForMember(dest => dest.AuthorizationScopeType, opt => opt.MapFrom(src => src.AuthorizationScope.AuthorizationScopeType))
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.AuthorizationScope.Id));

            Mapper.CreateMap<AuthorizationScope, AuthorizationScopeDTO>()
              .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.AuthorizationClaims, opt => opt.MapFrom(src => src.AuthorizationClaims));

            Mapper.CreateMap<AuthorizationClaim, AuthorizationClaimDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<NotificationRuleAlarmTypeDTO, NotificationRuleAlarmType>().ReverseMap();

            Mapper.CreateMap<SearchCriteriaDTO, SearchCriteria>();
            Mapper.CreateMap<RecipientSearchCriteriaDTO, RecipientSearchCriteria>();

            Mapper.CreateMap<PartRevision, PartRevisionLightDTO>().ReverseMap();

            Mapper.CreateMap<Part, PartLightDTO>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .AfterMap((src, dest) =>
            {
                if (src.PartRevisions != null && src.PartRevisions.Count() > 0)
                {
                    dest.PartRevisions = new List<PartRevisionLightDTO>();
                    foreach (var partRevision in src.PartRevisions)
                    {
                        if (partRevision != null)
                        {
                            dest.PartRevisions.Add(new PartRevisionLightDTO()
                            {
                                ID = partRevision.Id,
                                RevisionName = partRevision.RevisionName,
                                SelectedStatus = true
                            });
                        }
                    }

                    dest.SelectedStatus = true;
                    dest.PartRevisionsCount = (src.PartRevisions.Count() == 1 && src.PartRevisions.Where(x => x.RevisionName.Equals("Initial") || string.IsNullOrEmpty(x.RevisionName)).Count() == 1) ? 0 : src.PartRevisions.Count();
                }
            }).ReverseMap();

            Mapper.CreateMap<Page<Part>, PageDTO<PartLightDTO>>();

            Mapper.CreateMap<NotificationDTO, Notification>().ReverseMap();
            Mapper.CreateMap<UserNotificationDTO, UserNotification>().ReverseMap();
            Mapper.CreateMap<WorkstationNotificationDTO, WorkstationNotification>().ReverseMap();
            Mapper.CreateMap<NotificationPlaceholderDTO, NotificationPlaceholder>().ReverseMap();

            Mapper.CreateMap<AgentWorkstation, AgentWorkstationDTO>().ReverseMap();

            Mapper.CreateMap<AgentWorkstationPortMap, AgentWorkstationPortMapDTO>().ReverseMap();

            Mapper.CreateMap<Page<AgentWorkstation>, PageDTO<AgentWorkstationDTO>>().ReverseMap();

            Mapper.CreateMap<NotificationRuleRecipient, NotificationRuleRecipientDTO>().ReverseMap();

            Mapper.CreateMap<Page<NotificationRuleRecipient>, PageDTO<NotificationRuleRecipientDTO>>();

            Mapper.CreateMap<getStatisticalRuleTemplateListResult, StatisticalRuleTemplateDTO>();

            Mapper.CreateMap<Page<getStatisticalRuleTemplateListResult>, PageDTO<StatisticalRuleTemplateDTO>>();
            Mapper.CreateMap<ProcessingTemplateListResult, PostProcessingFactorDTO>()
                .ForMember(dest => dest.PostProcessingType, opt => opt.MapFrom(src => (PostProcessingType)src.PostProcessingOptionId));

            Mapper.CreateMap<ProcessingTemplateListResult, ProcessingTemplateDTO>()
                .ForMember(dest => dest.PostProcessingFactor, opt => opt.MapFrom(src => Mapper.Map<PostProcessingFactorDTO>(src)))
                .ForMember(dest => dest.NormalizationType, opt => opt.MapFrom(src => (NormalizationType)src.NormalizationOptions))
                .ForMember(dest => dest.FeatureType, opt => opt.MapFrom(src => (ProcessingTemplateFeatureType)src.FeatureType));

            Mapper.CreateMap<Page<ProcessingTemplateListResult>, PageDTO<ProcessingTemplateDTO>>();

            Mapper.CreateMap<Page<ProcessingTemplate>, PageDTO<ProcessingTemplateDTO>>();

            Mapper.CreateMap<ProcessingTemplateDTO, ProcessingTemplate>()
                .ForMember(dest => dest.FeatureType, opt => opt.MapFrom(src => (byte)src.FeatureType))
                .ForMember(dest => dest.NormalizationOptions, opt => opt.MapFrom(src => (byte)src.NormalizationType))
                .ForMember(dest => dest.PostProcessingOptionId, opt => opt.MapFrom(src => (byte?)src.PostProcessingFactor.PostProcessingType))
                .ForMember(dest => dest.F1, opt => opt.MapFrom(src => (decimal?)src.PostProcessingFactor.F1))
                .ForMember(dest => dest.F2, opt => opt.MapFrom(src => (decimal?)src.PostProcessingFactor.F2))
                .ForMember(dest => dest.F3, opt => opt.MapFrom(src => (decimal?)src.PostProcessingFactor.F3))
                .ForMember(dest => dest.F4, opt => opt.MapFrom(src => (decimal?)src.PostProcessingFactor.F4))
                .ForMember(dest => dest.F5, opt => opt.MapFrom(src => (decimal?)src.PostProcessingFactor.F5));

            Mapper.CreateMap<ProcessingTemplate, PostProcessingFactorDTO>()
                .ForMember(dest => dest.PostProcessingType, opt => opt.MapFrom(src => (PostProcessingType)src.PostProcessingOptionId))
                .ForMember(dest => dest.F1, opt => opt.MapFrom(src => src.F1))
                .ForMember(dest => dest.F2, opt => opt.MapFrom(src => src.F2))
                .ForMember(dest => dest.F3, opt => opt.MapFrom(src => src.F3))
                .ForMember(dest => dest.F4, opt => opt.MapFrom(src => src.F4))
                .ForMember(dest => dest.F5, opt => opt.MapFrom(src => src.F5));

            Mapper.CreateMap<ProcessingTemplate, ProcessingTemplateDTO>()
               .ForMember(dest => dest.FeatureType, opt => opt.MapFrom(src => (ProcessingTemplateFeatureType)src.FeatureType))
               .ForMember(dest => dest.NormalizationType, opt => opt.MapFrom(src => (NormalizationType)src.NormalizationOptions))
               .ForMember(dest => dest.PostProcessingFactor, opt => opt.MapFrom(src => Mapper.Map<PostProcessingFactorDTO>(src)));

            Mapper.CreateMap<NotificationTemplateMap, NotificationTemplateMapDTO>().ReverseMap();

            Mapper.CreateMap<getNotificationRuleParameterValues, DataStreamParameterValueDTO>();
            Mapper.CreateMap<getParameterSetParameterValues, DataStreamParameterValueDTO>()
                .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.ParameterId))
                .ForMember(dest => dest.EntityValue, opt => opt.MapFrom(src => src.ParameterValue));

            Mapper.CreateMap<getNotificationList, NotificationTileDTO>()
                .ForMember(dest => dest.NotificationRuleCategory, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.NotificationCreatedDate, opt => opt.MapFrom(src => src.CreatedDate));

            Mapper.CreateMap<Page<getNotificationList>, PageDTO<NotificationTileDTO>>();

            Mapper.CreateMap<getFinishedDataCollectionList, FinishedDataCollectionTileDTO>();

            Mapper.CreateMap<getFinishedDCDetail, FinishedDataCollectionTileDTO>();

            Mapper.CreateMap<Page<getFinishedDataCollectionList>, PageDTO<FinishedDataCollectionTileDTO>>();

            Mapper.CreateMap<UsersWorkstationsForDC, DataCollectionPublishEventDTO>().ForMember(dest => dest.DCEventId, opt => opt.MapFrom(src => (DCEventType)src.DCEventType));

            Mapper.CreateMap<UsersWorkstationsForInProgressDC, InProgressDataCollectionEventDTO>();

            Mapper.CreateMap<StatisticalRuleTemplate, EntityDependencyDTO>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ReverseMap();

            Mapper.CreateMap<ProcessesWithCurrentProcessState, ConditionProcessMapLightDTO>();

            Mapper.CreateMap<Page<getResponseGroupList_Result>, PageDTO<ResponseGroupDTO>>();

            Mapper.CreateMap<getResponseGroupList_Result, ResponseGroupDTO>()
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.ModifiedByUser }))
                .ForMember(dest => dest.ResponseType, opt => opt.MapFrom(src => src.ResponseType))
                .ReverseMap();

            Mapper.CreateMap<Page<getChecklistList_Result>, PageDTO<ChecklistDTO>>();

            Mapper.CreateMap<getAllResponsesByFeatureIDs_Result, ResponseDTO>();

            Mapper.CreateMap<getChecklistList_Result, ChecklistDTO>()
                .ForMember(dest => dest.ModifiedByUser, opt => opt.MapFrom(src => new UserLiteDTO { FirstName = src.ModifiedByUser }))
                .ReverseMap();

            Mapper.CreateMap<Page<AssociatedFeature>, PageDTO<AssociatedFeatureDTO>>();

            Mapper.CreateMap<AssociatedFeature, AssociatedFeatureDTO>()
               .ReverseMap();

            Mapper.CreateMap<ChecklistAccess, ChecklistAccessMapDTO>().ReverseMap();
            Mapper.CreateMap<ChecklistFeatureMap, ChecklistFeatureMapDTO>().ReverseMap();
            Mapper.CreateMap<ChecklistParameter, ChecklistParameterMapDTO>().ReverseMap();
            Mapper.CreateMap<ChecklistParameter, ChecklistParameterMapDTO>();
            Mapper.CreateMap<ChecklistParameter, DataStreamParameterDTO>()
                .ForMember(dest => dest.DataStreamConfigurationId, opt => opt.MapFrom(src => src.ChecklistId))
                .ForMember(dest => dest.DataStreamParameterValues, opt => opt.MapFrom(src => src.ChecklistParameterValues.Where(x => !x.IsDeleted)))
                .ReverseMap();
            Mapper.CreateMap<ChecklistParameterValue, DataStreamParameterValueDTO>()
                .ForMember(dest => dest.DataStreamParameterId, opt => opt.MapFrom(src => src.ChecklistParameterId))
                .ReverseMap();
            Mapper.CreateMap<Checklist, ChecklistDTO>()
             .ForMember(dest => dest.DataStreamParameters, opt => opt.MapFrom(src => src.ChecklistParameters));

            Mapper.CreateMap<Page<OperationDiagramFeatureMap>, PageDTO<OperationDiagramFeatureMapDTO>>().ReverseMap();
            Mapper.CreateMap<OperationDiagramFeatureMap, OperationDiagramFeatureMapDTO>().ReverseMap();

            Mapper.CreateMap<Page<DatabaseEntity>, PageDTO<EntityDTO>>();
            Mapper.CreateMap<DatabaseEntity, EntityDTO>().ReverseMap();

            Mapper.CreateMap<ChecklistFeature, ChecklistFeatureDTO>().ReverseMap();
            Mapper.CreateMap<Page<ChecklistFeature>, PageDTO<ChecklistFeatureDTO>>();

            Mapper.CreateMap<GaugeRecordItemsByGaugeDevice, GaugeFormatRecordDetailDTO>();

            Mapper.CreateMap<Page<GaugeRecordItemsByGaugeDevice>, PageDTO<GaugeFormatRecordDetailDTO>>();

            Mapper.CreateMap<EntityDependencyDTO, getChecklistAssociations_Result>().ReverseMap();
            Mapper.CreateMap<EntityAssociationMap, EntityDependencyDTO>();
            Mapper.CreateMap<Dashboard, DashboardDTO>()
                 .ForMember(dest => dest.DashboardType, opt => opt.MapFrom(src => src.Type))
                 .AfterMap((src, dest) =>
                 {
                     if (src.DashboardTagMaps != null && src.DashboardTagMaps.Count > 0)
                     {
                         List<TagValueDTO> tagValueDTOList = new List<TagValueDTO>();
                         foreach (var tagMap in src.DashboardTagMaps)
                         {
                             if (tagMap.TagValue != null)
                             {
                                 TagValueDTO tagValue = Mapper.Map<TagValueDTO>(tagMap.TagValue);
                                 tagValue.IsDeleted = tagMap.IsDeleted;
                                 tagValueDTOList.Add(tagValue);
                             }
                         }

                         dest.TagValues = tagValueDTOList;
                     }

                     if (src.DataSet != null)
                     {
                         dest.SelectedParameterSetInfo.ParameterSetId = src.DataSet.Id;
                         dest.SelectedParameterSetInfo.ParameterSetName = src.DataSet.Name;
                         dest.SelectedParameterSetInfo.SingleSite = src.DataSet.SingleSite;
                     }

                     if (src.DashboardLinks != null && src.DashboardLinks.Count > 0)
                     {
                         List<DashboardLinkDTO> linkDTOList = new List<DashboardLinkDTO>();

                         foreach (var link in src.DashboardLinks)
                         {
                             DashboardLinkDTO linkDTO = Mapper.Map<DashboardLinkDTO>(link.Dashboard1);
                             linkDTOList.Add(linkDTO);
                         }

                         dest.DashboardLinkItems.LinkedDashboards = linkDTOList;
                     }

                     if (src.DashboardLinkUrls != null && src.DashboardLinkUrls.Count > 0)
                     {
                         List<DashboardLinkUrlDTO> linkUrlDTOList = new List<DashboardLinkUrlDTO>();

                         foreach (var linkUrl in src.DashboardLinkUrls)
                         {
                             DashboardLinkUrlDTO linkDTO = Mapper.Map<DashboardLinkUrlDTO>(linkUrl);
                             linkUrlDTOList.Add(linkDTO);
                         }

                         dest.DashboardLinkItems.LinkedUrls = linkUrlDTOList;
                     }
                 });

            Mapper.CreateMap<Page<getAssociatedOperationDiagramByPartRevisionId_Result>, PageDTO<OperationDiagramDTO>>();
            Mapper.CreateMap<Page<getAssociatedDataCollectionByPartRevisionId_Result>, PageDTO<DCConfigurationDTO>>();
            Mapper.CreateMap<Page<OperationDiagram>, PageDTO<OperationDiagramDTO>>();
            Mapper.CreateMap<getFeatureDCLinkage_Result, FeatureDCAssociationDTO>()
                .ForMember(dst => dst.ID, conf => conf.MapFrom(src => Convert.ToInt32(src.Id, System.Globalization.CultureInfo.InvariantCulture)));
            Mapper.CreateMap<Page<getFeatureDCLinkage_Result>, PageDTO<FeatureDCAssociationDTO>>();
            Mapper.CreateMap<getFeatureDCAssociationDetail_Result, DCPartFamilyDTO>()
                .ForMember(dst => dst.ID, conf => conf.MapFrom(src => Convert.ToInt32(src.Id, System.Globalization.CultureInfo.InvariantCulture)));
            Mapper.CreateMap<Page<getFeatureDCAssociationDetail_Result>, PageDTO<DCPartFamilyDTO>>();

            Mapper.CreateMap<Page<TagValue>, PageDTO<TagValueDTO>>();
            Mapper.CreateMap<Page<OperationDiagram>, PageDTO<OperationDiagramDTO>>();
            Mapper.CreateMap<Page<SpecificationLimit>, PageDTO<SpecificationLimitDTO>>();
            Mapper.CreateMap<Page<DCConfiguration>, PageDTO<DCConfigurationDTO>>();
            Mapper.CreateMap<Page<DataSetTagMap>, PageDTO<DataSetTagMapDTO>>();
            Mapper.CreateMap<DataCollectionType, DataCollectionTypeDTO>().ReverseMap();

            Mapper.CreateMap<ChecklistEvent, ChecklistEventDTO>().ReverseMap();

            Mapper.CreateMap<ChecklistEventResponseMap, ChecklistEventResponseMapDTO>().ReverseMap();

            Mapper.CreateMap<ShiftPrcessLogs, ShiftLogDTO>().ReverseMap();
        }

        #region Private Helper Methods

        /// <summary>
        /// Get the Mapped ChecklistDTO from Checklist Access Entity
        /// </summary>
        /// <param name="src"> Checklist Entity</param>
        /// <param name="dest"> Checklist DTO</param>
        private void MapChecklistAccesses(Checklist src, ChecklistDTO dest)
        {
            if (src.ChecklistAccesses != null && src.ChecklistAccesses.Count > 0)
            {
                List<ChecklistAccessMapDTO> checklistAccessesDTO = new List<ChecklistAccessMapDTO>();
                foreach (var accessee in src.ChecklistAccesses)
                {
                    ChecklistAccessMapDTO checklistAccessMapDTO = Mapper.Map<ChecklistAccess, ChecklistAccessMapDTO>(accessee);
                    if (checklistAccessMapDTO.RoleId != null && accessee.Role != null)
                    {
                        checklistAccessMapDTO.Name = accessee.Role.Name;
                        checklistAccessMapDTO.UserCountInRole = accessee.Role.UserRoleMaps.ToList().Count;
                    }
                    else if (checklistAccessMapDTO.UserId != null && accessee.User != null)
                    {
                        checklistAccessMapDTO.Name = accessee.User.FirstName + " " + accessee.User.LastName;
                    }
                    else if (checklistAccessMapDTO.WorkstationId != null && accessee.Workstation != null)
                    {
                        checklistAccessMapDTO.Name = accessee.Workstation.Name;
                    }

                    checklistAccessesDTO.Add(checklistAccessMapDTO);
                }

                dest.AssignmentCollection = checklistAccessesDTO;
            }
        }

        /// <summary>
        /// Get the Mapped ChecklistDTO from Checklist Feature Entity
        /// </summary>
        /// <param name="src"> Checklist Entity</param>
        /// <param name="dest"> Checklist DTO</param>
        private void MapChecklistFeatures(Checklist src, ChecklistDTO dest)
        {
            if (src.ChecklistFeatureMaps != null && src.ChecklistFeatureMaps.Count > 0)
            {
                List<ChecklistFeatureMapDTO> checklistFeaturesMapDTO = new List<ChecklistFeatureMapDTO>();
                foreach (var feautre in src.ChecklistFeatureMaps)
                {
                    ChecklistFeatureMapDTO checklistFeatureMapDTO = Mapper.Map<ChecklistFeatureMap, ChecklistFeatureMapDTO>(feautre);
                    checklistFeaturesMapDTO.Add(checklistFeatureMapDTO);
                }

                dest.Features = checklistFeaturesMapDTO;
                dest.FeatureCount = src.ChecklistFeatureMaps.Count;
            }
        }

        /// <summary>
        /// Get the Mapped ChecklistDTO from Checklist Process Entity
        /// </summary>
        /// <param name="src"> Checklist Entity</param>
        /// <param name="dest"> Checklist DTO</param>
        private void MapChecklistProcesses(Checklist src, ChecklistDTO dest)
        {
            if (src.ChecklistParameters != null && src.ChecklistParameters.Count > 0)
            {
                List<DataStreamParameterDTO> checklistParametersDTO = new List<DataStreamParameterDTO>();
                foreach (var parameter in src.ChecklistParameters)
                {
                    DataStreamParameterDTO checklistParameterMapDTO = Mapper.Map<ChecklistParameter, DataStreamParameterDTO>(parameter);
                    checklistParametersDTO.Add(checklistParameterMapDTO);
                }

                dest.DataStreamParameters = checklistParametersDTO;
            }
        }

        /// <summary>
        /// Get the Mapped ChecklistDTO from Checklist Tag Entity
        /// </summary>
        /// <param name="src"> Checklist Entity</param>
        /// <param name="dest"> Checklist DTO</param>
        private void MapChecklistTags(Checklist src, ChecklistDTO dest)
        {
            if (src.ChecklistTagMaps != null && src.ChecklistTagMaps.Count > 0)
            {
                List<TagValueDTO> tagValues = new List<TagValueDTO>();
                foreach (var tagMap in src.ChecklistTagMaps)
                {
                    if (tagMap.TagValue != null)
                    {
                        TagValueDTO tagValue = Mapper.Map<TagValueDTO>(tagMap.TagValue);
                        tagValue.IsDeleted = tagMap.IsDeleted;
                        tagValues.Add(tagValue);
                    }
                }

                dest.TagValues = tagValues;
            }
        }

        #endregion
    }
}
