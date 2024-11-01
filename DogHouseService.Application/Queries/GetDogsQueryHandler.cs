﻿using AutoMapper;
using DogHouseService.Application.DTOs;
using DogHouseService.Domain.Entities;
using DogHouseService.Domain.Interfaces;
using MediatR;
using System.Reflection;

namespace DogHouseService.Application.Queries.GetDogs
{
    public class GetDogsQueryHandler : IRequestHandler<GetDogsQuery, IEnumerable<DogDto>>
    {
        private readonly IDogRepository _dogRepository;
        private readonly IMapper _mapper;

        public GetDogsQueryHandler(IDogRepository dogRepository, IMapper mapper)
        {
            _dogRepository = dogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DogDto>> Handle(GetDogsQuery request, CancellationToken cancellationToken)
        {
            var dogs = await _dogRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(request.SortAttribute))
            {
                var propertyInfo = typeof(Dog).GetProperty(request.SortAttribute,
                                  BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo != null)
                {
                    dogs = request.SortOrder.ToLower() == "desc"
                        ? dogs.OrderByDescending(d => propertyInfo.GetValue(d))
                        : dogs.OrderBy(d => propertyInfo.GetValue(d));
                }
            }

            dogs = dogs.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

            return _mapper.Map<IEnumerable<DogDto>>(dogs);
        }

    }
}
