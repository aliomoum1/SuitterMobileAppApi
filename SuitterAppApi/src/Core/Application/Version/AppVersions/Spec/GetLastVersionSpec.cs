using SuitterAppApi.Application.Version.AppVersions.Dtos;
using SuitterAppApi.Domain.Version;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuitterAppApi.Application.Version.AppVersions.Spec;
public class GetLastVersionSpec : Specification<AppVersion, AppVersionDetailsDto>, ISingleResultSpecification
{
    public GetLastVersionSpec() => Query.OrderByDescending(x=>x.CreatedOn).Take(1);
}
