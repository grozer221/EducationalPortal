import React, {useCallback, useEffect, useState} from 'react';
import {useMutation, useQuery} from '@apollo/client';
import {
    GET_EDUCATIONAL_YEARS_QUERY,
    GetEducationalYearsData,
    GetEducationalYearsVars,
} from '../../educationalYears.queries';
import {ColumnsType} from 'antd/es/table';
import {EducationalYear} from '../../educationalYears.types';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {Col, message, Row, Space, Table, Tag} from 'antd';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {createSearchParams, Link, useNavigate, useSearchParams} from 'react-router-dom';
import {
    REMOVE_EDUCATIONAL_YEAR_MUTATION,
    RemoveEducationalYearData,
    RemoveEducationalYearVars,
} from '../../educationalYears.mutations';
import {stringToUkraineDate} from '../../../../../../convertors/stringToDatetimeConvertors';
import Title from 'antd/es/typography/Title';
import Search from 'antd/es/input/Search';
import debounce from 'lodash.debounce';
import '../../../../../../styles/controls.css';

export const EducationalYearsIndex = () => {
    const [searchParams] = useSearchParams();
    const [page, setPage] = useState(parseInt(searchParams.get('page') || '') || 1);
    const [like, setLike] = useState(searchParams.get('like') || '');
    const [likeInput, setLikeInput] = useState(searchParams.get('like') || '');
    const navigate = useNavigate();
    const getEducationalYearsQuery = useQuery<GetEducationalYearsData, GetEducationalYearsVars>(GET_EDUCATIONAL_YEARS_QUERY,
        {variables: {page, like}, fetchPolicy: 'network-only'},
    );
    const [removeEducationalYearsMutation, removeEducationalYearsMutationOptions] = useMutation<RemoveEducationalYearData, RemoveEducationalYearVars>(REMOVE_EDUCATIONAL_YEAR_MUTATION);

    useEffect(() => {
        navigate({
            pathname: './',
            search: `?${createSearchParams({page: page.toString(), like})}`,
        });
        getEducationalYearsQuery.refetch({page, like});
    }, [page, like]);

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
                // isAdministrator()
                //     ? <ButtonsVUR viewUrl={`${educationalYear.id}`} updateUrl={`update/${educationalYear.id}`}
                //                   onRemove={() => onRemove(educationalYear.id)}/>
                //     : <ButtonsVUR viewUrl={`${educationalYear.id}`}/>
                <ButtonsVUR viewUrl={`${educationalYear.id}`} updateUrl={`update/${educationalYear.id}`}
                            onRemove={() => onRemove(educationalYear.id)}/>
            ),
        },
    ];

    const debouncedSearchEduYearsHandler = useCallback(debounce(setLike, 500), []);
    const searchEduYearsHandler = (value: string) => {
        debouncedSearchEduYearsHandler(value);
        setLikeInput(value);
    };


    return (
        <Space size={20} direction={'vertical'} style={{width: '100%'}}>
            <Title level={2}>Навчальні роки</Title>
            {/*{isAdministrator() &&*/}
            {/*<Link to={'create'}>*/}
            {/*    <ButtonCreate/>*/}
            {/*</Link>*/}
            {/*}*/}
            <Row justify="space-between">
                <Col>
                    <Link to={'create'}>
                        <ButtonCreate/>
                    </Link>
                </Col>
                <Col>
                    <Search
                        value={likeInput}
                        onChange={e => searchEduYearsHandler(e.target.value)}
                        placeholder="Пошук"
                        enterButton
                        loading={getEducationalYearsQuery.loading}
                        className={'search'}
                    />
                </Col>
            </Row>
            <Table
                style={{width: '100%'}}
                rowKey={'id'}
                loading={getEducationalYearsQuery.loading || removeEducationalYearsMutationOptions.loading}
                dataSource={getEducationalYearsQuery.data?.getEducationalYears.entities}
                columns={columns}
                pagination={{
                    total: getEducationalYearsQuery.data?.getEducationalYears.total,
                    onChange: setPage,
                }}
            />
        </Space>
    );
};
