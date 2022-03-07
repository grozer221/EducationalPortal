import React, {useState} from 'react';
import {useMutation, useQuery} from '@apollo/client';
import {
    GET_EDUCATIONAL_YEARS_QUERY,
    GetEducationalYearsData,
    GetEducationalYearsVars,
} from '../../educationalYears.queries';
import {ColumnsType} from 'antd/es/table';
import {EducationalYear} from '../../educationalYears.types';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {message, Space, Table, Tag} from 'antd';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {Link, useNavigate} from 'react-router-dom';
import {
    REMOVE_EDUCATIONAL_YEAR_MUTATION,
    RemoveEducationalYearData,
    RemoveEducationalYearVars,
} from '../../educationalYears.mutations';
import {stringToUkraineDate} from '../../../../../../convertors/stringToDatetimeConvertors';
import Title from 'antd/es/typography/Title';
import {isAdministrator} from '../../../../../../utils/permissions';

export const EducationalYearsIndex = () => {
    const [page, setPage] = useState(1);
    const navigate = useNavigate();
    const getEducationalYearsQuery = useQuery<GetEducationalYearsData, GetEducationalYearsVars>(GET_EDUCATIONAL_YEARS_QUERY,
        {variables: {page: page}, fetchPolicy: 'network-only'},
    );
    const [removeEducationalYearsMutation, removeEducationalYearsMutationOptions] = useMutation<RemoveEducationalYearData, RemoveEducationalYearVars>(REMOVE_EDUCATIONAL_YEAR_MUTATION);

    const onRemove = (educationalYearId: string) => {
        removeEducationalYearsMutation({variables: {id: educationalYearId}})
            .then(async (response) => {
                await getEducationalYearsQuery.refetch({page});
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    const columns: ColumnsType<EducationalYear> = [
        {
            title: 'Назва',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: 'Поточний',
            dataIndex: 'isCurrent',
            key: 'isCurrent',
            render: (text: string, educationalYear: EducationalYear) =>
                <>{educationalYear?.isCurrent
                    ? <Tag color="green">Так</Tag>
                    : <Tag color="red">Ні</Tag>
                }</>,
        },
        {
            title: 'Дата початку',
            dataIndex: 'dateStart',
            key: 'dateStart',
            render: (text: string, educationalYear: EducationalYear) => <>{stringToUkraineDate(educationalYear.dateStart)}</>,
        },
        {
            title: 'Дата кінця',
            dataIndex: 'dateEnd',
            key: 'dateEnd',
            render: (text: string, educationalYear: EducationalYear) => <>{stringToUkraineDate(educationalYear.dateEnd)}</>,
        },
        {
            title: 'Дії',
            dataIndex: 'actions',
            key: 'actions',
            render: (text: string, educationalYear: EducationalYear) => (
                isAdministrator()
                    ? <ButtonsVUR viewUrl={`${educationalYear.id}`} updateUrl={`update/${educationalYear.id}`}
                                  onRemove={() => onRemove(educationalYear.id)}/>
                    : <ButtonsVUR viewUrl={`${educationalYear.id}`}/>
            ),
        },
    ];

    return (
        <Space size={20} direction={'vertical'} style={{width: '100%'}}>
            <Title level={2}>Навчальні роки</Title>
            {isAdministrator() &&
            <Link to={'create'}>
                <ButtonCreate/>
            </Link>
            }
            <Table
                style={{width: '100%'}}
                rowKey={'id'}
                loading={getEducationalYearsQuery.loading || removeEducationalYearsMutationOptions.loading}
                dataSource={getEducationalYearsQuery.data?.getEducationalYears.entities}
                columns={columns}
                pagination={{
                    total: getEducationalYearsQuery.data?.getEducationalYears.total,
                    onChange: async (newPage: number) => {
                        setPage(newPage);
                        await getEducationalYearsQuery.refetch({
                            page: newPage,
                        });
                    },
                }}
            />
        </Space>
    );
};
