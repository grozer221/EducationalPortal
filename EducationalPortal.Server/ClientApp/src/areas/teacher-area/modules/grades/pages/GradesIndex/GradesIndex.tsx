import React, {useCallback, useEffect, useState} from 'react';
import {useLazyQuery, useMutation} from '@apollo/client';
import {ColumnsType} from 'antd/es/table';
import {ButtonsVUR} from '../../../../../../components/ButtonsVUD/ButtonsVUR';
import {Col, message, Row, Space, Table} from 'antd';
import {ButtonCreate} from '../../../../../../components/ButtonCreate/ButtonCreate';
import {createSearchParams, Link, useNavigate, useSearchParams} from 'react-router-dom';
import {GET_GRADES_QUERY, GetGradesData, GetGradesVars} from '../../grades.queries';
import {Grade} from '../../grades.types';
import {REMOVE_GRADE_MUTATION, RemoveGradeData, RemoveGradeVars} from '../../grades.mutations';
import Title from 'antd/es/typography/Title';
import debounce from 'lodash.debounce';
import Search from 'antd/es/input/Search';


export const GradesIndex = () => {
    const [searchParams] = useSearchParams();
    const pageParamsValue = parseInt(searchParams.get('page') || '') || 1;
    const likeParamsValue = searchParams.get('like') || '';
    const [page, setPage] = useState(pageParamsValue);
    const [like, setLike] = useState(likeParamsValue);
    const [likeInput, setLikeInput] = useState(searchParams.get('like') || '');
    const [getGrades, getGradesOptions] = useLazyQuery<GetGradesData, GetGradesVars>(GET_GRADES_QUERY,
        {variables: {page: page, like: like}, fetchPolicy: 'network-only'},
    );
    const [removeGradeMutation, removeGradeMutationOptions] = useMutation<RemoveGradeData, RemoveGradeVars>(REMOVE_GRADE_MUTATION);
    const navigate = useNavigate();

    useEffect(() => {
        navigate({search: `?${createSearchParams({page: page.toString(), like})}`});
        getGrades({variables: {page, like}});
    }, [page, like]);

    useEffect(() => setPage(pageParamsValue), [pageParamsValue]);
    useEffect(() => {
        setLike(likeParamsValue);
        setLikeInput(likeParamsValue);
    }, [likeParamsValue]);

    const onRemove = (gradeId: string) => {
        removeGradeMutation({variables: {id: gradeId}})
            .then(async (response) => {
                await getGrades({variables: {page, like}});
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    const columns: ColumnsType<Grade> = [
        {
            title: 'Назва',
            dataIndex: 'name',
            key: 'name',
        },
        {
            title: 'Дії',
            dataIndex: 'actions',
            key: 'actions',
            render: (text, grade) => (
                // isAdministrator()
                //     ? <ButtonsVUR viewUrl={`${grade?.id}`} updateUrl={`update/${grade?.id}`}
                //                   onRemove={() => onRemove(grade?.id)}/>
                //     : <ButtonsVUR viewUrl={`${grade?.id}`}/>
                <ButtonsVUR viewUrl={`${grade?.id}`} updateUrl={`update/${grade?.id}`}
                            onRemove={() => onRemove(grade?.id)}/>
            ),
        },
    ];

    const debouncedSearchGradesHandler = useCallback(debounce(setLike, 500), []);
    const searchGradesHandler = (value: string) => {
        debouncedSearchGradesHandler(value);
        setLikeInput(value);
    };

    return (
        <Space size={20} direction={'vertical'} style={{width: '100%'}}>
            <Title level={2}>Класи</Title>
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
                        onChange={e => searchGradesHandler(e.target.value)}
                        placeholder="Пошук"
                        enterButton
                        loading={getGradesOptions.loading}
                        className={'search'}
                    />
                </Col>
            </Row>
            <Table
                rowKey={'id'}
                loading={getGradesOptions.loading || removeGradeMutationOptions.loading}
                dataSource={getGradesOptions.data?.getGrades.entities}
                columns={columns}
                pagination={{
                    total: getGradesOptions.data?.getGrades.total,
                    onChange: setPage,
                }}
            />
        </Space>
    );
};
